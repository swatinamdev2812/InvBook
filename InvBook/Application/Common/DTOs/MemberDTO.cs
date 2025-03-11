namespace InvBook.Application.Common.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int BookingCount { get; set; }
    }

}
