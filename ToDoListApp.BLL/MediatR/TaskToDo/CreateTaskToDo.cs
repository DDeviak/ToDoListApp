using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record CreateTaskToDoCommand(TaskToDoCreateDTO TaskToDo) : IRequest<Result<TaskToDoDTO>>;

    public class CreateTaskToDoCommandHandler : IRequestHandler<CreateTaskToDoCommand, Result<TaskToDoDTO>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public CreateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TaskToDoDTO>> Handle(CreateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo taskToDo = _mapper.Map<TaskToDo>(request.TaskToDo);
            taskToDo = await _taskToDoRepository.CreateAsync(taskToDo);
            return Result.Ok(_mapper.Map<TaskToDoDTO>(taskToDo));
        }
    }
}