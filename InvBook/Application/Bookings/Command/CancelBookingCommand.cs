using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Bookings.Command
{
    public class CancelBookingCommand : IRequest<string>
    {
        public int BookingId { get; set; }
    }

    public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, string>
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<InventoryItem> _inventoryRepository;

        public CancelBookingHandler(IRepository<Booking> bookingRepository, IRepository<InventoryItem> inventoryRepository)
        {
            _bookingRepository = bookingRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<string> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null)
                return "Booking not found";

            var item = await _inventoryRepository.GetByIdAsync(booking.InventoryItemId);
            if (item != null)
            {
                item.RemainingCount++;
                _inventoryRepository.Update(item);

                await _inventoryRepository.SaveChangesAsync();
            }

            _bookingRepository.Delete(booking);
            await _bookingRepository.SaveChangesAsync();

            return "Booking cancelled";
        }
    }

}
