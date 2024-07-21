using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTaskToDoQuery(Guid Id) : IRequest<Result<TaskToDoDTO?>>;

    public class GetTaskToDoQueryHandler : IRequestHandler<GetTaskToDoQuery, Result<TaskToDoDTO?>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoQueryHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TaskToDoDTO?>> Handle(GetTaskToDoQuery request, CancellationToken cancellationToken)
        {
            TaskToDo? taskToDo = await _taskToDoRepository.GetByIdAsync(request.Id);
            return Result.Ok(taskToDo is null ? null : _mapper.Map<TaskToDoDTO>(taskToDo));
        }
    }
}