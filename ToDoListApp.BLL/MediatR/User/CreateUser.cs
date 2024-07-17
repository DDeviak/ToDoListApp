using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.User
{
    using ToDoListApp.DAL.Models;
    public record CreateUserCommand(string Username, string Email, string PasswordHash) : IRequest<User>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            return await _userRepository.CreateAsync(user);
        }
    }
}