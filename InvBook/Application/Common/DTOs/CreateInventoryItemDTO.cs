using CsvHelper.Configuration.Attributes;
using InvBook.Shared.Utilities;

namespace InvBook.Application.Common.DTOs
{
    public class CreateInventoryItemDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }

        [TypeConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExpirationDate { get; set; }
    }
}
