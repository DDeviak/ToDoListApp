using ToDoListApp.DAL.Repositories.Interfaces;
using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using ToDoListApp.DAL.Models;
    public record CreateTaskToDoCommand(string Title, string Description, int TasklistId, DateTime Deadline) : IRequest<TaskToDo>;

    public class CreateTaskToDoCommandHandler : IRequestHandler<CreateTaskToDoCommand, TaskToDo>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public CreateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<TaskToDo> Handle(CreateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            var taskToDo = new TaskToDo
            {
                Title = request.Title,
                Description = request.Description,
                TasklistId = request.TasklistId,
                Deadline = request.Deadline
            };

            return await _taskToDoRepository.CreateAsync(taskToDo);
        }
    }
}