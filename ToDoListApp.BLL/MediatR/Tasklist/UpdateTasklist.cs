using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    public record UpdateTasklistCommand(TasklistDTO Tasklist) : IRequest<TasklistDTO>;

    public class UpdateTasklistCommandHandler : IRequestHandler<UpdateTasklistCommand, TasklistDTO>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public UpdateTasklistCommandHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<TasklistDTO> Handle(UpdateTasklistCommand request, CancellationToken cancellationToken)
        {
            Tasklist tasklist = _mapper.Map<Tasklist>(request.Tasklist);
            await _tasklistRepository.UpdateAsync(tasklist);
            return request.Tasklist;
        }
    }
}