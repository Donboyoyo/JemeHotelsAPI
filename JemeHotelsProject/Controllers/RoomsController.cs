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
        private readonly IWebHostEnvironment webHostEnvironment;

        public RoomsController(JemeHotelsDbContext jemeHotelsDbContext, IMapper mapper, IRoomRepository roomRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.jemeHotelsDbContext = jemeHotelsDbContext;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
            this.webHostEnvironment = webHostEnvironment;
        }


        // GET: api/Rooms
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRooms([FromQuery] string? room_type)
        {
            var roomsDomain = await roomRepository.GetAllAsync(room_type);

            var roomsDto = mapper.Map<List<RoomDTO>>(roomsDomain);

            return Ok(roomsDto);
        }



        // This is the endpoint for adding a room
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom([FromForm] AddRoomRequestDto addRoomRequestDto)
        {
            var roomExists = await jemeHotelsDbContext.Rooms.AnyAsync(r => r.roomNumber == addRoomRequestDto.RoomNumber);

            if (roomExists)
            {
                return Conflict("Room with inputed room number already exists");
            }

            var imageUrls = new List<string>();

            if (addRoomRequestDto.Images != null && addRoomRequestDto.Images.Count > 0)
            {
                var localPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "rooms");
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }

                foreach (var image in addRoomRequestDto.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(localPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    var urlFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/rooms/{fileName}";
                    imageUrls.Add(urlFilePath);
                }
            }

            var roomDomain = mapper.Map<Room>(addRoomRequestDto);

            roomDomain.Image = imageUrls.Any() ? string.Join(",", imageUrls) : null;

            roomDomain = await roomRepository.CreateAsync(roomDomain);

            var roomsDto = mapper.Map<RoomDTO>(roomDomain);

            return Ok(roomsDto);
        }


    }
}
