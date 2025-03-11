namespace InvBook.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public DateTime BookingDate { get; set; }

        public virtual Member Member { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
