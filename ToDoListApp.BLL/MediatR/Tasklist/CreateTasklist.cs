using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    public record CreateTasklistCommand(TasklistCreateDTO Tasklist) : IRequest<TasklistDTO>;

    public class CreateTasklistCommandHandler : IRequestHandler<CreateTasklistCommand, TasklistDTO>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public CreateTasklistCommandHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<TasklistDTO> Handle(CreateTasklistCommand request, CancellationToken cancellationToken)
        {
            Tasklist tasklist = _mapper.Map<Tasklist>(request.Tasklist);
            tasklist = await _tasklistRepository.CreateAsync(tasklist);
            return _mapper.Map<TasklistDTO>(tasklist);
        }
    }
}