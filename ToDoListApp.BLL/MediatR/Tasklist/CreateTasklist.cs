using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record CreateTasklistCommand(TasklistCreateDTO Tasklist) : IRequest<Result<TasklistDTO>>;

    public class CreateTasklistCommandHandler : IRequestHandler<CreateTasklistCommand, Result<TasklistDTO>>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public CreateTasklistCommandHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<Result<TasklistDTO>> Handle(CreateTasklistCommand request, CancellationToken cancellationToken)
        {
            Tasklist tasklist = _mapper.Map<Tasklist>(request.Tasklist);
            tasklist = await _tasklistRepository.CreateAsync(tasklist);
            return Result.Ok(_mapper.Map<TasklistDTO>(tasklist));
        }
    }
}