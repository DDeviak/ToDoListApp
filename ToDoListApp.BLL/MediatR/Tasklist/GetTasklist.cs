using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.BLL.Mapping;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTasklistQuery(Guid Id) : IRequest<Result<TasklistDTO>>;

    public class GetTasklistQueryHandler : IRequestHandler<GetTasklistQuery, Result<TasklistDTO>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTasklistQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<TasklistDTO>> Handle(GetTasklistQuery request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.Id);
            if(tasklist is null)
            {
                return Result.Fail<TasklistDTO>("Tasklist not found");
            }

            return Result.Ok(_mapper.Map<TasklistDTO>(tasklist));
        }
    }
}