using MediatR;

namespace ToDoListApp.BLL.MediatR.Tasklist
{
    using AutoMapper;
    using FluentResults;
    using ToDoListApp.BLL.DTO.Tasklist;
    using ToDoListApp.DAL.Models;
    using ToDoListApp.DAL.Repositories.Interfaces;

    public record GetTasklistsByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<TasklistDTO>>>;

    public class GetTasklistsByUserQueryHandler : IRequestHandler<GetTasklistsByUserQuery, Result<IEnumerable<TasklistDTO>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetTasklistsByUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TasklistDTO>>> Handle(GetTasklistsByUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                return Result.Fail("User not found");
            }

            return Result.Ok(_mapper.ProjectTo<TasklistDTO>((user.Tasklists ?? []).AsQueryable()).AsEnumerable());
        }
    }
}