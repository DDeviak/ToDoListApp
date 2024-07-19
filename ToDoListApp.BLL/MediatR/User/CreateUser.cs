using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.User
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.User;
    using ToDoListApp.DAL.Models;
    public record CreateUserCommand(UserCreateDTO User) : IRequest<UserDTO>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.User);
            user = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDTO>(user);
        }
    }
}