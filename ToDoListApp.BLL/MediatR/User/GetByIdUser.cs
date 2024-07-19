using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.User
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.User;
    using ToDoListApp.DAL.Models;
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDTO?>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.Id);
            return _mapper.Map<UserDTO?>(user);
        }
    }
}