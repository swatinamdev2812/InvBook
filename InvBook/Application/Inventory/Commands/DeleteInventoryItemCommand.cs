using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Inventory.Commands
{
    public class DeleteInventoryItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteInventoryItemHandler : IRequestHandler<DeleteInventoryItemCommand, bool>
    {
        private readonly IRepository<InventoryItem> _repository;

        public DeleteInventoryItemHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteById(request.Id);
        }
    }
}
