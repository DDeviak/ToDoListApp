using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using ToDoListApp.DAL.Models;
    public record UpdateTasklistCommand(Guid Id, string Title, Guid UserId) : IRequest<Tasklist>;

    public class UpdateTasklistCommandHandler : IRequestHandler<UpdateTasklistCommand, Tasklist>
    {
        private readonly ITasklistRepository _tasklistRepository;

        public UpdateTasklistCommandHandler(ITasklistRepository tasklistRepository)
        {
            _tasklistRepository = tasklistRepository;
        }

        public async Task<Tasklist> Handle(UpdateTasklistCommand request, CancellationToken cancellationToken)
        {
            var tasklist = new Tasklist
            {
                Id = request.Id,
                Title = request.Title,
                UserId = request.UserId
            };

            await _tasklistRepository.UpdateAsync(tasklist);
            return tasklist;
        }
    }
}