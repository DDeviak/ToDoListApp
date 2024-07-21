using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTasklistQuery(Guid Id) : IRequest<Result<TasklistDTO?>>;

    public class GetTasklistQueryHandler : IRequestHandler<GetTasklistQuery, Result<TasklistDTO?>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTasklistQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<TasklistDTO?>> Handle(GetTasklistQuery request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.Id);
            return Result.Ok(tasklist is null ? null : _mapper.Map<TasklistDTO>(tasklist));
        }
    }
}