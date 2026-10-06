using AutoMapper;
using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using JemeHotelsProject.Models.DTOs;
using JemeHotelsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JemeHotelsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly JemeHotelsDbContext dbContext;
        private readonly IGuestRepository guestRepository;
        private readonly IMapper mapper;

        public GuestsController(JemeHotelsDbContext dbContext, IGuestRepository guestRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }


        // Get guests
        // api/Guests?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetGuests([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            // Get data from database
            var guestDomainModel = await guestRepository.GetAllAsync(pageNumber, pageSize);

            // Use automapper to map domain models to DTOs
            var guestsDto = mapper.Map<List<GuestDTO>>(guestDomainModel);

            // Return DTOs
            return Ok(guestsDto);
        }

        // Get guest by ID
        [HttpGet]
        [ActionName("GetByIdAsync")]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var guestDomain = await guestRepository.GetByIdAsync(id);

            if (guestDomain == null)
            {
                return NotFound();
            }

            var guestDto = mapper.Map<GuestDTO>(guestDomain);

            return Ok(guestDto);
        }

        //// Add guest
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> AddGuest([FromBody] AddGuestRequestDto addGuestRequestDto)
        //{
        //    // Convert DTO to domain model
        //    var guestDomainModel = mapper.Map<Guest>(addGuestRequestDto);

        //    // Add domain model to the database
        //    guestDomainModel = await guestRepository.CreateAsync(guestDomainModel);

        //    // Convert domain model to DTO
        //    var guestDto = mapper.Map<GuestDTO>(guestDomainModel);

            
        //    // Return response to user
        //    return CreatedAtAction(nameof(GetByIdAsync), new { id = guestDto.GuestID }, guestDto);
        //}
    }
}
