using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    public record GetTasklistQuery(Guid Id) : IRequest<TasklistDTO?>;

    public class GetTasklistQueryHandler : IRequestHandler<GetTasklistQuery, TasklistDTO?>
    {
        private readonly ITasklistRepository _tasklistRepository;
        private readonly IMapper _mapper;

        public GetTasklistQueryHandler(ITasklistRepository tasklistRepository, IMapper mapper)
        {
            _tasklistRepository = tasklistRepository;
            _mapper = mapper;
        }

        public async Task<TasklistDTO?> Handle(GetTasklistQuery request, CancellationToken cancellationToken)
        {
            Tasklist? tasklist = await _tasklistRepository.GetByIdAsync(request.Id);
            return _mapper.Map<TasklistDTO?>(tasklist);
        }
    }
}