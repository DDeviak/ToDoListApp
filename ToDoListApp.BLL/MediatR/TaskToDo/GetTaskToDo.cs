using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using ToDoListApp.DAL.Models;
    public record GetTaskToDoQuery(Guid Id) : IRequest<TaskToDo?>;

    public class GetTaskToDoQueryHandler : IRequestHandler<GetTaskToDoQuery, TaskToDo?>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public GetTaskToDoQueryHandler(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<TaskToDo?> Handle(GetTaskToDoQuery request, CancellationToken cancellationToken)
        {
            return await _taskToDoRepository.GetByIdAsync(request.Id);
        }
    }
}