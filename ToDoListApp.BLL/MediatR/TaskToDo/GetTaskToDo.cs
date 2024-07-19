using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.BLL.DTO.TaskToDo;
    public record GetTaskToDoQuery(Guid Id) : IRequest<TaskToDoCreateDTO?>;

    public class GetTaskToDoQueryHandler : IRequestHandler<GetTaskToDoQuery, TaskToDoCreateDTO?>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoQueryHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<TaskToDoCreateDTO?> Handle(GetTaskToDoQuery request, CancellationToken cancellationToken)
        {
            TaskToDo? taskToDo = await _taskToDoRepository.GetByIdAsync(request.Id);
            return _mapper.Map<TaskToDoCreateDTO>(taskToDo);
        }
    }
}