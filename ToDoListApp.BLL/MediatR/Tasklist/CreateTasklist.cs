using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using ToDoListApp.DAL.Models;
    public record CreateTasklistCommand(string Title, int UserId) : IRequest<Tasklist>;

    public class CreateTasklistCommandHandler : IRequestHandler<CreateTasklistCommand, Tasklist>
    {
        private readonly ITasklistRepository _tasklistRepository;

        public CreateTasklistCommandHandler(ITasklistRepository tasklistRepository)
        {
            _tasklistRepository = tasklistRepository;
        }

        public async Task<Tasklist> Handle(CreateTasklistCommand request, CancellationToken cancellationToken)
        {
            var tasklist = new Tasklist
            {
                Title = request.Title,
                UserId = request.UserId
            };

            return await _tasklistRepository.CreateAsync(tasklist);
        }
    }
}