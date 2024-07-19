using MediatR;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    public record GetTasklistsByUserQuery(Guid UserId) : IRequest<IEnumerable<TasklistDTO>>;

    public class GetTasklistsByUserQueryHandler : IRequestHandler<GetTasklistsByUserQuery, IEnumerable<TasklistDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetTasklistsByUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TasklistDTO>> Handle(GetTasklistsByUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.UserId);
            return _mapper.ProjectTo<TasklistDTO>(user?.Tasklists.AsQueryable());
        }
    }
}