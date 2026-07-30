using JemeHotelsProject.Models.DTOs;
using JemeHotelsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JemeHotelsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.roleManager = roleManager;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                return Ok(new { message = "User has been registered successfully" });
                // Add roles to this user (Reader, Writer, or both)
                //if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                //{
                //    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                //    if (identityResult.Succeeded)
                //    {
                //        return Ok("User was registered! Please login.");
                //    }
                //}
            }

            return BadRequest("Something went wrong.");
        }




        // Creating a login method
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
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

                        return Ok(response);
                    }

                }

            }
            return BadRequest("Username or Password incorrect");
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
