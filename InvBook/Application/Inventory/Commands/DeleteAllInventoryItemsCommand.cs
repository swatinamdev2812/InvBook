using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Inventory.Commands
{
    public class DeleteAllInventoryItemsCommand : IRequest
    {
    }

    public class DeleteAllInventoryItemsHandler : IRequestHandler<DeleteAllInventoryItemsCommand>
    {
        private readonly IRepository<InventoryItem> _repository;

        public DeleteAllInventoryItemsHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteAllInventoryItemsCommand request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();

            if (items.Any())
            {
                _repository.DeleteRangeAndSaveChanges(items);
            }

            return Unit.Value;
        }
    }
}
