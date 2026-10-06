using AutoMapper;
using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using JemeHotelsProject.Models.DTOs;
using JemeHotelsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JemeHotelsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly JemeHotelsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IBookingRepository bookingRepository;

        public BookingsController(JemeHotelsDbContext dbContext, IMapper mapper, IBookingRepository bookingRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
        }

        

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBooking([FromBody] AddBookingRequestDto addBookingRequestDto)
    {
        var guestId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (guestId == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var roomInput = await dbContext.Rooms.FirstOrDefaultAsync(x => x.roomID == addBookingRequestDto.RoomId);
        if (roomInput == null)
        {
            return NotFound("Room is unavailable or does not exist");
        }

        var days = (addBookingRequestDto.EndDate.Date - addBookingRequestDto.StartDate.Date).Days + 1;
        if (days <= 0)
        {
            return BadRequest("End date must be after start date.");
        }

        var bookingDomainModel = mapper.Map<Booking>(addBookingRequestDto);

        bookingDomainModel.GuestId = guestId;
        bookingDomainModel.totalBill = days * roomInput.price;
        bookingDomainModel.isPaid = false;

        bookingDomainModel = await bookingRepository.CreateBookingAsync(bookingDomainModel);
        await dbContext.SaveChangesAsync();

        return Ok(mapper.Map<returnBookingDetailsDTO>(bookingDomainModel));
    }



    // To get all bookings 
    [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetallBookings()
        {
            var bookingsDomain = await bookingRepository.GetAllBookingAsync();

            var bookingsDto = mapper.Map<List<returnBookingDetailsDTO>>(bookingsDomain);

            return Ok(bookingsDto);
        }

    }
}
