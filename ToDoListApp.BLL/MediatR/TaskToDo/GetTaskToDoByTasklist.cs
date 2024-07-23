using MediatR;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using FluentResults;
    using Microsoft.EntityFrameworkCore;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTaskToDoByTasklistQuery(Guid TasklistId) : IRequest<Result<IEnumerable<TaskToDoDTO>>>;

    public class GetTaskToDoByTasklistQueryHandler : IRequestHandler<GetTaskToDoByTasklistQuery, Result<IEnumerable<TaskToDoDTO>>>
    {
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoByTasklistQueryHandler(ITaskToDoRepository taskToDoRepository, IMapper mapper)
        {
            _taskToDoRepository = taskToDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TaskToDoDTO>>> Handle(GetTaskToDoByTasklistQuery request, CancellationToken cancellationToken)
        {
            return Result.Ok(_mapper.ProjectTo<TaskToDoDTO>((await _taskToDoRepository.GetAllAsync()).Where(t => t.TasklistId == request.TasklistId).AsQueryable()).AsEnumerable());
        }
    }
}