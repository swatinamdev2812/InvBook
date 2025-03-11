using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Members.Command
{
    public class CreateMemberCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, int>
    {
        private readonly IRepository<Member> _repository;

        public CreateMemberHandler(IRepository<Member> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = new Member
            {
                Name = request.Name,
                Surname = request.Surname,
                DateOfJoining = request.DateOfJoining
            };

            await _repository.AddAsync(member);
            await _repository.SaveChangesAsync();
            return member.Id;
        }
    }

}
