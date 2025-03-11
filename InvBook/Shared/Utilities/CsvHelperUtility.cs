using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InvBook.Application.Common.DTOs;
using System.Globalization;

namespace InvBook.Shared.Utilities
{
    public static class CsvHelperUtility<T>
    {
        public static IEnumerable<T> ParseCsv(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header?.Replace(" ", "").Replace("_", "").ToLowerInvariant()
            });

            return csv.GetRecords<T>().ToList();
        }
    }

    public class CustomDateTimeConverter : DateTimeConverter
    {
        private static readonly string[] formats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" };

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) =>
            DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                ? date
                : throw new CsvHelperException(row.Context, $"Invalid DateTime format: {text}");
    }

}