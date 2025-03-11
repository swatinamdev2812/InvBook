using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Bookings.Command
{
    public class BookItemCommand : IRequest<string>
    {
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
    }

    public class BookItemHandler : IRequestHandler<BookItemCommand, string>
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<InventoryItem> _inventoryRepository;
        private const int MAX_BOOKINGS = 2;

        public BookItemHandler(IRepository<Booking> bookingRepository, IRepository<InventoryItem> inventoryRepository)
        {
            _bookingRepository = bookingRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<string> Handle(BookItemCommand request, CancellationToken cancellationToken)
        {
            var memberBookings = (await _bookingRepository.GetAllAsync(b => b.MemberId == request.MemberId)).Count();

            if (memberBookings >= MAX_BOOKINGS)
                return "Member has reached the maximum booking limit";

            var item = await _inventoryRepository.GetByIdAsync(request.InventoryItemId);
            if (item == null || item.RemainingCount <= 0)
                return "Item not available";

            var booking = new Booking
            {
                MemberId = request.MemberId,
                InventoryItemId = request.InventoryItemId,
                BookingDate = DateTime.UtcNow
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            item.RemainingCount--;
            _inventoryRepository.Update(item);
            await _inventoryRepository.SaveChangesAsync();

            return "Booking successful with Booking Id: " + booking.Id;
        }
    }

}
