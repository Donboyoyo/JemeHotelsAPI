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
    public class RoomsController : ControllerBase
    {
        private readonly JemeHotelsDbContext jemeHotelsDbContext;
        private readonly IMapper mapper;
        private readonly IRoomRepository roomRepository;

        public RoomsController(JemeHotelsDbContext jemeHotelsDbContext, IMapper mapper, IRoomRepository roomRepository)
        {
            this.jemeHotelsDbContext = jemeHotelsDbContext;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
        }


        // GET: api/Rooms
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRooms()
        {
            var roomsDomain = await roomRepository.GetAllAsync();

            var roomsDto = mapper.Map<List<RoomDTO>>(roomsDomain);

            return Ok(roomsDto);
        }



        // This is the endpoint for adding a room
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom(AddRoomRequestDto addRoomRequestDto)
        {
            var roomExists = await jemeHotelsDbContext.Rooms.AnyAsync(r => r.roomNumber == addRoomRequestDto.roomNumber);

            if (roomExists)
            {
                return Conflict("Room with inputed room number already exists");
            }
            var roomDomain = mapper.Map<Room>(addRoomRequestDto);

            roomDomain = await roomRepository.CreateAsync(roomDomain);

            var roomsDto = mapper.Map<RoomDTO>(roomDomain);

            return Ok(roomsDto);
        }

        // This is the endpoint for getting all available rooms
        [HttpGet]
        [Route("available-rooms")]

        public async Task<IActionResult> GetAvailableRooms()
        {
            var roomsDomain = await roomRepository.GetAllAvailableAsync();

            var roomsDto = mapper.Map<List<RoomDTO>>(roomsDomain);

            return Ok(roomsDto);
        }
    }
}
