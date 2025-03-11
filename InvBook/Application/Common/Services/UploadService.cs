using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using InvBook.Shared.Utilities;

public class UploadService : IUploadService
{
    private readonly IRepository<InventoryItem> _inventoryRepository;
    private readonly IMapper _mapper;

    public UploadService(IRepository<InventoryItem> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
    }

    public async Task UploadInventoryCsvAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        using var stream = file.OpenReadStream();

        var inventoryDtos = CsvHelperUtility<CreateInventoryItemDTO>.ParseCsv(stream);

        var inventoryItems = _mapper.Map<List<InventoryItem>>(inventoryDtos);

        await _inventoryRepository.AddRangeAsync(inventoryItems);
        await _inventoryRepository.SaveChangesAsync();
    }
}
