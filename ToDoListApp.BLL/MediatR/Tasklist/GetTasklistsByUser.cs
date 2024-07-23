using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTasklistsByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<TasklistDTO>>>;

    public class GetTasklistsByUserQueryHandler : IRequestHandler<GetTasklistsByUserQuery, Result<IEnumerable<TasklistDTO>>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTasklistsByUserQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TasklistDTO>>> Handle(GetTasklistsByUserQuery request, CancellationToken cancellationToken)
        {
            return Result.Ok(_mapper.ProjectTo<TasklistDTO>((await _tasklistRepository.GetAllAsync()).Where(t => t.UserId == request.UserId).AsQueryable()).AsEnumerable());
        }
    }
}