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

        //// To create a booking
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> CreateBooking([FromBody] AddBookingRequestDto addBookingRequestDto)
        //{
        //    //check if room exist first
        //    var roomInput = await dbContext.Rooms.FirstOrDefaultAsync(x => x.roomID == addBookingRequestDto.RoomId);
        //    if((roomInput == null))
        //    {
        //        return NotFound("Room is unavailable or does not exist");
        //    }

        //    var guestInput = await dbContext.Guests.FirstOrDefaultAsync(x => x.GuestID == addBookingRequestDto.GuestId);
        //    if (guestInput == null)
        //    {
        //        return NotFound("Guest does not exist");
        //    }

        //    var bookingDomainModel = mapper.Map<Booking>(addBookingRequestDto);

        //    bookingDomainModel = await bookingRepository.CreateBookingAsync(bookingDomainModel);

        //    await dbContext.SaveChangesAsync();

        //    return Ok(mapper.Map<returnBookingDetailsDTO>(bookingDomainModel));
            
        //}

        // To create booking, from Gemini

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBooking([FromBody] AddBookingRequestDto addBookingRequestDto)
    {
        // 1. Extract GuestId from the logged-in user's JWT Token securely
        var guestId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (guestId == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        // 2. Check if room exists and fetch it to get its price
        var roomInput = await dbContext.Rooms.FirstOrDefaultAsync(x => x.roomID == addBookingRequestDto.RoomId);
        if (roomInput == null)
        {
            return NotFound("Room is unavailable or does not exist");
        }

        // 3. Calculate price: ((endDate - startDate) + 1) * pricePerNight
        var days = (addBookingRequestDto.EndDate.Date - addBookingRequestDto.StartDate.Date).Days + 1;
        if (days <= 0)
        {
            return BadRequest("End date must be after start date.");
        }

        // 4. Map the DTO to the Domain Model
        var bookingDomainModel = mapper.Map<Booking>(addBookingRequestDto);

        // 5. Manually override the calculated and secure fields
        bookingDomainModel.GuestId = guestId;
        bookingDomainModel.totalBill = days * roomInput.price;
        bookingDomainModel.isPaid = false;

        // 6. Save using your repository
        bookingDomainModel = await bookingRepository.CreateBookingAsync(bookingDomainModel);
        await dbContext.SaveChangesAsync();

        // 7. Return the response
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
