using AutoMapper;
using JemeHotelsProject.Models.Domain;
using JemeHotelsProject.Models.DTOs;

namespace JemeHotelsProject.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Guest, GuestDTO>().ReverseMap();
            CreateMap<AddGuestRequestDto, Guest>().ReverseMap();
            CreateMap<RoomDTO, Room>().ReverseMap();
            CreateMap<AddRoomRequestDto, Room>().ReverseMap();
            CreateMap<AddBookingRequestDto, Booking>().ReverseMap();
            CreateMap<returnBookingDetailsDTO, Booking>().ReverseMap();
            CreateMap<BookingDTO, Booking>().ReverseMap();

        }
    }
}
