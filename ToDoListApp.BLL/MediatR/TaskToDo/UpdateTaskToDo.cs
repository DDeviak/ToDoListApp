using ToDoListApp.DAL.Repositories.Interfaces;
using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    public record UpdateTaskToDoCommand(TaskToDoDTO TaskToDo) : IRequest<TaskToDoDTO>;

    public class UpdateTaskToDoCommandHandler : IRequestHandler<UpdateTaskToDoCommand, TaskToDoDTO>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;
        public UpdateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<TaskToDoDTO> Handle(UpdateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo taskToDo = _mapper.Map<TaskToDo>(request.TaskToDo);
            await _taskToDoRepository.UpdateAsync(taskToDo);
            return request.TaskToDo;
        }
    }
}