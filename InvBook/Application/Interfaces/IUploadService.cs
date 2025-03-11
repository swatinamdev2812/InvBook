namespace InvBook.Application.Interfaces
{
    public interface IUploadService
    {
        Task UploadInventoryCsvAsync(IFormFile file);
    }
}
