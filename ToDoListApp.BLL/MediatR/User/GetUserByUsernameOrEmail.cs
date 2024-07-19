using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.User
{
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using AutoMapper;
    using ToDoListApp.BLL.DTO.User;
    using ToDoListApp.DAL.Models;
    public record GetUserByUsernameOrEmailQuery(string UsernameOrEmail) : IRequest<UserDTO?>;
    public class GetUserByUsernameOrEmailQueryHandler : IRequestHandler<GetUserByUsernameOrEmailQuery, UserDTO?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameOrEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<User,bool>> predicate;
            if (Regex.IsMatch(request.UsernameOrEmail, @"[^@\s]+@[^@\s]+\.[^@\s]{1,3}"))
            {
                predicate = t => request.UsernameOrEmail == t.Email;
            }
            else
            {
                predicate = t => request.UsernameOrEmail == t.Username;
            }
            User? user = await _userRepository.GetSingleOrDefaultAsync(predicate);
            return _mapper.Map<UserDTO?>(user);
        }
    }
}