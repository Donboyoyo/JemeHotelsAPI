using AutoMapper.Configuration.Annotations;
using JemeHotelsProject.Data;
using JemeHotelsProject.Models.DTOs;
using JemeHotelsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace JemeHotelsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            ITokenRepository tokenRepository, IEmailService emailService, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.roleManager = roleManager;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        //initilize register
        //send otp to user and save relation to db email otp

        // POST: /api/Auth/Register 
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var existingUser = await userManager.FindByEmailAsync(registerRequestDto.Email);

            if (existingUser != null)
            {
                return Conflict("User with email already exists");
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };


            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var param = new Dictionary<string, string?>
                {
                    {"token", token },
                    {"email", identityUser.Email }
                };

                var clientUri = configuration["ClientURILink:EmailConfirmationURI"];
                var callback = QueryHelpers.AddQueryString(clientUri!, param);


                await emailService.SendEmailAsync(
                    identityUser.Email,
                    "Confirm your email",
                    $"Hello {identityUser.UserName}, <br><br>" + 
                    $"Your account has been registered successfully <br><br>" + 
                    $"Please click <a href= '{callback}'> here </a> to confirm your email"
                    );
                
                return Ok(new { message = "User has been registered successfully" });

            }

            return BadRequest("Something went wrong.");
        }



        // Client URI should be https://localhost:7208/api/Auth/email-confirmation
        [HttpGet("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid Email Confirmation Request");
            }

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                return BadRequest("Invalid Email Confirmation Request");
            }

            return Ok();

        }



        // Creating a login method
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    return Unauthorized("Email is not confirmed");
                }

                if (await userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized("The account is locked out");
                }

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (!checkPasswordResult)
                {
                    await userManager.AccessFailedAsync(user);
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        var content = $"Your account is locked out. If you want to reset the password, " +
                            $"you can use the reset password link on the login page";

                        await emailService.SendEmailAsync(
                            loginRequestDto.Email,
                            "Locked out account information", 
                            content);

                        return Unauthorized("The account is locked out"); 
                    }

                    return Unauthorized("Username or password incorrect");
                }

                else if (checkPasswordResult)
                {
                    // Getting the roles for the user to use in the token creation
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        // Creating a new login response type and then populate it
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        await userManager.ResetAccessFailedCountAsync(user);

                        return Ok(response);
                    }

                }

            }
            return BadRequest("Username or Password incorrect");
        }


        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPassword.Email }
            };

            var clientUri = configuration["ClientURILink:ResetpasswordURI"];
            var callback = QueryHelpers.AddQueryString(clientUri!, param);

            await emailService.SendEmailAsync(
                user.Email,
                "Reset password Token",
                callback);

            return Ok(new
            {
                email = user.Email,
                token = token
            });
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            var result = await userManager.ResetPasswordAsync(user, resetPassword.Token!, resetPassword.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            await userManager.SetLockoutEndDateAsync(user, null);


            return Ok();
            
        }




        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("add-role")]

        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role added successfully" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Role already exists");
        }


        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("assign-role")]

        public async Task<IActionResult> AssignRole([FromBody] userRole model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }
            return BadRequest(result.Errors);
        }
    }
}
