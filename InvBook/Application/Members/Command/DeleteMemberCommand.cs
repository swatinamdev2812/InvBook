using InvBook.Application.Interfaces;
using InvBook.Domain;
using MediatR;

namespace InvBook.Application.Members.Command
{
    public class DeleteMemberCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, bool>
    {
        private readonly IRepository<Member> _repository;

        public DeleteMemberHandler(IRepository<Member> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteById(request.Id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }
    }

}
