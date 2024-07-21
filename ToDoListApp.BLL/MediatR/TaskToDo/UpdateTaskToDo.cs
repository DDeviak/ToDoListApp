using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record UpdateTaskToDoCommand(TaskToDoDTO TaskToDo) : IRequest<Result<TaskToDoDTO>>;

    public class UpdateTaskToDoCommandHandler : IRequestHandler<UpdateTaskToDoCommand, Result<TaskToDoDTO>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public UpdateTaskToDoCommandHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TaskToDoDTO>> Handle(UpdateTaskToDoCommand request, CancellationToken cancellationToken)
        {
            TaskToDo taskToDo = _mapper.Map<TaskToDo>(request.TaskToDo);
            await _taskToDoRepository.UpdateAsync(taskToDo);
            return Result.Ok(_mapper.Map<TaskToDoDTO>(taskToDo));
        }
    }
}