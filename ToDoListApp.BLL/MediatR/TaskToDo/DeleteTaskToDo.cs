using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using FluentResults;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record DeleteTaskToDoCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteTaskToDoCommandHandler : IRequestHandler<DeleteTaskToDoCommand, Result<Unit>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public DeleteTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo? taskToDo = await _taskToDoRepository.GetByIdAsync(request.Id);
            if (taskToDo is not null)
            {
                await _taskToDoRepository.DeleteAsync(taskToDo);
            }

            return Result.Ok(Unit.Value);
        }
    }
}