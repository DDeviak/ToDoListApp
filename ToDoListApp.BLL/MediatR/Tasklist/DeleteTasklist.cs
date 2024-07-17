using ToDoListApp.DAL.Repositories.Interfaces;
using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using ToDoListApp.DAL.Models;
    public record DeleteTasklistCommand(int Id) : IRequest<Unit>;

    public class DeleteTasklistCommandHandler : IRequestHandler<DeleteTasklistCommand, Unit>
    {
        private readonly ITasklistRepository _tasklistRepository;

        public DeleteTasklistCommandHandler(ITasklistRepository tasklistRepository)
        {
            _tasklistRepository = tasklistRepository;
        }

        public async Task<Unit> Handle(DeleteTasklistCommand request, CancellationToken cancellationToken)
        {
            var tasklist = await _tasklistRepository.GetByIdAsync(request.Id);
            if (tasklist is not null) await _tasklistRepository.DeleteAsync(tasklist);
            return Unit.Value;
        }
    }
}

