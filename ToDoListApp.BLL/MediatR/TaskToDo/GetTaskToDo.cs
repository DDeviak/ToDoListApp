using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTaskToDoQuery(Guid Id) : IRequest<Result<TaskToDoDTO>>;

    public class GetTaskToDoQueryHandler : IRequestHandler<GetTaskToDoQuery, Result<TaskToDoDTO>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoQueryHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TaskToDoDTO>> Handle(GetTaskToDoQuery request, CancellationToken cancellationToken)
        {
            TaskToDo? taskToDo = await _taskToDoRepository.GetByIdAsync(request.Id);
            if (taskToDo is null)
            {
                return Result.Fail<TaskToDoDTO>("TaskToDo not found");
            }

            return Result.Ok(_mapper.Map<TaskToDoDTO>(taskToDo));
        }
    }
}