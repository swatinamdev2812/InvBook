using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Inventory.Queries
{
    public class GetAllInventoryItemsQuery : IRequest<List<InventoryItemDTO>> { }

    public class GetAllInventoryItemsHandler : IRequestHandler<GetAllInventoryItemsQuery, List<InventoryItemDTO>>
    {
        private readonly IRepository<InventoryItem> _repository;
        private readonly IMapper _mapper;

        public GetAllInventoryItemsHandler(IRepository<InventoryItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InventoryItemDTO>> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<List<InventoryItemDTO>>(items);
        }
    }
}
