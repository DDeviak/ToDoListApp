using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.TaskToDo
{
    using AutoMapper;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.BLL.DTO.TaskToDo;
    using Microsoft.EntityFrameworkCore;

    public record GetTaskToDoByTasklistQuery(Guid TasklistId) : IRequest<IEnumerable<TaskToDoDTO>>;

    public class GetTaskToDoByTasklistQueryHandler : IRequestHandler<GetTaskToDoByTasklistQuery, IEnumerable<TaskToDoDTO>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTaskToDoByTasklistQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskToDoDTO>> Handle(GetTaskToDoByTasklistQuery request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.TasklistId);
            return _mapper.ProjectTo<TaskToDoDTO>(tasklist?.Tasks.AsQueryable());
        }
    }
}