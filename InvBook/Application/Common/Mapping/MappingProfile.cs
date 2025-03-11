using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Domain;

namespace InvBook.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from Entity to DTO
            CreateMap<InventoryItem, InventoryItemDTO>();

            // Mapping from DTO to Entity
            CreateMap<CreateInventoryItemDTO, InventoryItem>();

            CreateMap<Member, MemberDTO>()
              .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => src.Bookings.Count));
        }
    }
}