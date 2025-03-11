using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Bookings.Queries
{
    public class GetBookingsByMemberIdQuery : IRequest<List<BookingDTO>>
    {
        public int MemberId { get; set; }
        public GetBookingsByMemberIdQuery(int memberId) => MemberId = memberId;
    }

    public class GetBookingsByMemberIdHandler : IRequestHandler<GetBookingsByMemberIdQuery, List<BookingDTO>>
    {
        private readonly IRepository<Booking> _bookingRepository;

        public GetBookingsByMemberIdHandler(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingDTO>> Handle(GetBookingsByMemberIdQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllAsync(b => b.MemberId == request.MemberId);

            return bookings
                .Select(b => new BookingDTO
                {
                    Id = b.Id,
                    MemberName = $"{b.Member.Name} {b.Member.Surname}",
                    InventoryItemName = b.InventoryItem.Title,
                    BookingDate = b.BookingDate
                })
                .ToList();
        }
    }

}
