using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using FluentResults;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record DeleteTasklistCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteTasklistCommandHandler : IRequestHandler<DeleteTasklistCommand, Result<Unit>>
    {
        private readonly ITasklistRepository _tasklistRepository;

        public DeleteTasklistCommandHandler(ITasklistRepository tasklistRepository)
        {
            _tasklistRepository = tasklistRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteTasklistCommand request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.Id);
            if (tasklist is not null)
            {
                await _tasklistRepository.DeleteAsync(tasklist);
            }

            return Result.Ok(Unit.Value);
        }
    }
}