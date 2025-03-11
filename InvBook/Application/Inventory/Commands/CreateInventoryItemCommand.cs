using AutoMapper;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Inventory.Commands
{
    public class CreateInventoryItemCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class CreateInventoryItemHandler : IRequestHandler<CreateInventoryItemCommand, bool>
    {
        private readonly IRepository<InventoryItem> _repository;
        private readonly IMapper _mapper;

        public CreateInventoryItemHandler(IRepository<InventoryItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var inventoryItem = _mapper.Map<InventoryItem>(request);
            await _repository.AddAsync(inventoryItem);

            return true;
        }
    }
}
