using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Members.Queries
{
    public class GetAllMembersQuery : IRequest<IEnumerable<MemberDTO>>
    {
    }

    public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberDTO>>
    {
        private readonly IRepository<Member> _repository;
        private readonly IMapper _mapper;

        public GetAllMembersHandler(IRepository<Member> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDTO>>(members);
        }
    }


}
