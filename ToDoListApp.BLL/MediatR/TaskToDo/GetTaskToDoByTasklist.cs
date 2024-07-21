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
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoByTasklistQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TaskToDoDTO>>> Handle(GetTaskToDoByTasklistQuery request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.TasklistId);
            return Result.Ok(_mapper.ProjectTo<TaskToDoDTO>(tasklist?.Tasks.AsQueryable()).AsEnumerable());
        }
    }
}