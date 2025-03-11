namespace InvBook.Application.Common.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string InventoryItemName { get; set; }
        public DateTime BookingDate { get; set; }
    }

}
