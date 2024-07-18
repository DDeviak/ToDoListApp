using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using ToDoListApp.DAL.Models;
    public record GetTasklistQuery(Guid Id) : IRequest<Tasklist?>;

    public class GetTasklistQueryHandler : IRequestHandler<GetTasklistQuery, Tasklist?>
    {
        private readonly ITasklistRepository _tasklistRepository;

        public GetTasklistQueryHandler(ITasklistRepository tasklistRepository)
        {
            _tasklistRepository = tasklistRepository;
        }

        public async Task<Tasklist?> Handle(GetTasklistQuery request, CancellationToken cancellationToken)
        {
            return await _tasklistRepository.GetByIdAsync(request.Id);
        }
    }
}