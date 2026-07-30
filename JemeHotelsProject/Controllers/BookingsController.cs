using AutoMapper;
using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using JemeHotelsProject.Models.DTOs;
using JemeHotelsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // To create a booking
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBooking([FromBody] AddBookingRequestDto addBookingRequestDto)
        {
            //check if room exist first
            var roomInput = await dbContext.Rooms.FirstOrDefaultAsync(x => x.roomID == addBookingRequestDto.RoomId);
            if((roomInput == null) || (roomInput.isAvailable == false))
            {
                return NotFound("Room is unavailable or does not exist");
            }

            var guestInput = await dbContext.Guests.FirstOrDefaultAsync(x => x.GuestID == addBookingRequestDto.GuestId);
            if (guestInput == null)
            {
                return NotFound("Guest does not exist");
            }

            var bookingDomainModel = mapper.Map<Booking>(addBookingRequestDto);

            bookingDomainModel = await bookingRepository.CreateBookingAsync(bookingDomainModel);

            roomInput.isAvailable = false;
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


        // To delete a booking
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var bookingsDomainModel = await bookingRepository.DeleteBookingAsync(id);

            if (bookingsDomainModel == null)
            {
                return NotFound();
            }
            var bookingsDto = mapper.Map<BookingDTO>(bookingsDomainModel);

            return Ok(bookingsDto);

        }
    }
}
