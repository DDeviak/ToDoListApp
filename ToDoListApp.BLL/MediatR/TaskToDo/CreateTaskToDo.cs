using ToDoListApp.DAL.Repositories.Interfaces;
using ToDoListApp.BLL.DTO.TaskToDo;
using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    public record CreateTaskToDoCommand(TaskToDoCreateDTO TaskToDo) : IRequest<TaskToDoCreateDTO>;

    public class CreateTaskToDoCommandHandler : IRequestHandler<CreateTaskToDoCommand, TaskToDoCreateDTO>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public CreateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<TaskToDoCreateDTO> Handle(CreateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo taskToDo = _mapper.Map<TaskToDo>(request.TaskToDo);
            taskToDo = await _taskToDoRepository.CreateAsync(taskToDo);
            return _mapper.Map<TaskToDoCreateDTO>(taskToDo);
        }
    }
}