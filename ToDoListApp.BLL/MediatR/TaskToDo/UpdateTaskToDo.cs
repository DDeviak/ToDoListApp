using ToDoListApp.DAL.Repositories.Interfaces;
using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using ToDoListApp.DAL.Models;
    public record UpdateTaskToDoCommand(int Id, string Title, string Description, int TasklistId, DateTime Deadline, TaskStatus Status) : IRequest<TaskToDo>;

    public class UpdateTaskToDoCommandHandler : IRequestHandler<UpdateTaskToDoCommand, TaskToDo>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public UpdateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<TaskToDo> Handle(UpdateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            var taskToDo = new TaskToDo
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                TasklistId = request.TasklistId,
                Deadline = request.Deadline,
                Status = request.Status
            };
            await _taskToDoRepository.UpdateAsync(taskToDo);
            return taskToDo;
        }
    }
}