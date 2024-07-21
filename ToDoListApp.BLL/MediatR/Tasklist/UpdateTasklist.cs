using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record UpdateTasklistCommand(TasklistDTO Tasklist) : IRequest<Result<TasklistDTO>>;

    public class UpdateTasklistCommandHandler : IRequestHandler<UpdateTasklistCommand, Result<TasklistDTO>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public UpdateTasklistCommandHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<TasklistDTO>> Handle(UpdateTasklistCommand request, CancellationToken cancellationToken)
        {
            Tasklist tasklist = _mapper.Map<Tasklist>(request.Tasklist);
            await _tasklistRepository.UpdateAsync(tasklist);
            return Result.Ok(request.Tasklist);
        }
    }
}