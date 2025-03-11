using System.ComponentModel.DataAnnotations.Schema;

namespace InvBook.Domain
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfJoining { get; set; }
        public virtual List<Booking> Bookings { get; set; } = new();

        [NotMapped]
        public int BookingCount => Bookings?.Count ?? 0;
    }
}
