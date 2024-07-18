using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.User
{
    using System.Text.RegularExpressions;
    using ToDoListApp.DAL.Models;
    public record GetUserByUsernameOrEmailQuery(string UsernameOrEmail) : IRequest<User?>;
    public class GetUserByUsernameOrEmailQueryHandler : IRequestHandler<GetUserByUsernameOrEmailQuery, User?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameOrEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
        {
            if (Regex.IsMatch(request.UsernameOrEmail, @"[^@\s]+@[^@\s]+\.[^@\s]{1,3}"))
            {
                return await _userRepository.GetSingleOrDefaultAsync(t => request.UsernameOrEmail == t.Email);
            }
            else
            {
                return await _userRepository.GetSingleOrDefaultAsync(t => request.UsernameOrEmail == t.Username);
            }
        }
    }
}