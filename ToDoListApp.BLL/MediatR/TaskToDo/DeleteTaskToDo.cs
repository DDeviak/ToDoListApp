using ToDoListApp.DAL.Repositories.Interfaces;
using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using ToDoListApp.DAL.Models;
    public record DeleteTaskToDoCommand(Guid Id) : IRequest<Unit>;

    public class DeleteTaskToDoCommandHandler : IRequestHandler<DeleteTaskToDoCommand, Unit>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public DeleteTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<Unit> Handle(DeleteTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo? taskToDo = await _taskToDoRepository.GetByIdAsync(request.Id);
            if (taskToDo is not null) await _taskToDoRepository.DeleteAsync(taskToDo);
            return Unit.Value;
        }
    }
}