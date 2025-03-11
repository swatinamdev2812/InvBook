using AutoMapper;
using InvBook.Application.Common.DTOs;
using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Members.Queries
{
    public class GetMemberByIdQuery : IRequest<MemberDTO?>
    {
        public int Id { get; set; }
    }

    public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDTO?>
    {
        private readonly IRepository<Member> _repository;
        private readonly IMapper _mapper;

        public GetMemberByIdHandler(IRepository<Member> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemberDTO?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _repository.GetByIdAsync(request.Id);
            return member != null ? _mapper.Map<MemberDTO>(member) : null;
        }
    }


}
