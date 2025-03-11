using InvBook.Application.Bookings.Command;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using Moq;

namespace InvBook.Tests
{
    public class BookingTests
    {
        private readonly Mock<IRepository<Booking>> _mockBookingRepository;
        private readonly Mock<IRepository<InventoryItem>> _mockInventoryRepository;
        private readonly Mock<IRepository<Member>> _mockMemberRepository;

        public BookingTests()
        {
            _mockBookingRepository = new Mock<IRepository<Booking>>();
            _mockInventoryRepository = new Mock<IRepository<InventoryItem>>();
            _mockMemberRepository = new Mock<IRepository<Member>>();
        }

        [Fact]
        public async Task Handle_BookItem_WhenItemAvailable_ShouldSucceed()
        {
            // Arrange
            int memberId = 1, inventoryItemId = 1;

            var member = new Member { Id = memberId, Name = "John", Surname = "Doe" };
            var inventoryItem = new InventoryItem { Id = inventoryItemId, Title = "Laptop", RemainingCount = 5 };

            _mockMemberRepository.Setup(repo => repo.GetByIdAsync(memberId)).ReturnsAsync(member);
            _mockInventoryRepository.Setup(repo => repo.GetByIdAsync(inventoryItemId)).ReturnsAsync(inventoryItem);
            _mockBookingRepository.Setup(repo => repo.GetAllAsync(b => b.MemberId == memberId))
                                  .ReturnsAsync(new List<Booking>());

            var handler = new BookItemHandler(_mockBookingRepository.Object, _mockInventoryRepository.Object);

            // Act
            var result = await handler.Handle(new BookItemCommand() { MemberId = memberId, InventoryItemId = inventoryItemId }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockBookingRepository.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Once);
            _mockInventoryRepository.Verify(repo => repo.Update(It.IsAny<InventoryItem>()), Times.Once);
        }


        [Fact(Skip = "Skipping this test for now")]
        public async Task Handle_BookItem_WhenMemberHasMaxBookings_ShouldFail()
        {
            // Arrange
            int memberId = 1, inventoryItemId = 1;
            var bookings = new List<Booking>
            {
                new Booking { Id = 101, MemberId = memberId, InventoryItemId = 2, BookingDate = DateTime.UtcNow },
                new Booking { Id = 102, MemberId = memberId, InventoryItemId = 3, BookingDate = DateTime.UtcNow }
            };

            _mockBookingRepository.Setup(repo => repo.GetAllAsync(b => b.MemberId == memberId))
                                  .ReturnsAsync(bookings);

            var handler = new BookItemHandler(_mockBookingRepository.Object, _mockInventoryRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(new BookItemCommand() { MemberId = memberId, InventoryItemId = inventoryItemId }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_CancelBooking_ShouldSucceed()
        {
            // Arrange
            int bookingId = 1;
            var booking = new Booking
            {
                Id = bookingId,
                MemberId = 1,
                InventoryItemId = 1,
                BookingDate = DateTime.UtcNow
            };
            var inventoryItem = new InventoryItem { Id = 1, Title = "Laptop", RemainingCount = 2 };

            _mockBookingRepository.Setup(repo => repo.GetByIdAsync(bookingId)).ReturnsAsync(booking);
            _mockInventoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(inventoryItem);

            var handler = new CancelBookingHandler(_mockBookingRepository.Object, _mockInventoryRepository.Object);

            // Act
            await handler.Handle(new CancelBookingCommand() { BookingId = bookingId }, CancellationToken.None);

            // Assert
            _mockBookingRepository.Verify(repo => repo.Delete(It.IsAny<Booking>()), Times.Once);
            _mockInventoryRepository.Verify(repo => repo.Update(It.IsAny<InventoryItem>()), Times.Once);
        }
    }

}