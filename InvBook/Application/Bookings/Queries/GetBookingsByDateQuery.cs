using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Bookings.Queries
{
    public class GetBookingsByDateQuery : IRequest<IEnumerable<BookingDTO>>
    {
        public DateTime Date { get; set; } = DateTime.Today;

        public GetBookingsByDateQuery(DateTime? date = null)
        {
            if (date != null)
                Date = date.Value.Date;
        }
    }

    public class GetBookingsByDateHandler : IRequestHandler<GetBookingsByDateQuery, IEnumerable<BookingDTO>>
    {
        private readonly IRepository<Booking> _bookingRepository;

        public GetBookingsByDateHandler(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<BookingDTO>> Handle(GetBookingsByDateQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllAsync(b => b.BookingDate.Date == request.Date);

            return bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                MemberName = b.Member.Name + " " + b.Member.Surname,
                InventoryItemName = b.InventoryItem.Title,
                BookingDate = b.BookingDate
            }).ToList();
        }
    }


}
