using InvBook.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvBook.API.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("inventory-csv")]
        public async Task<IActionResult> UploadInventoryCsv(IFormFile file)
        {
            await _uploadService.UploadInventoryCsvAsync(file);
            return Ok(new { message = "Inventory CSV uploaded successfully!" });
        }
    }
}