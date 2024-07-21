using MediatR;

namespace ToDoListApp.BLL.MediatR.User
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using AutoMapper;
    using FluentResults;
    using Microsoft.AspNetCore.Identity;
    using ToDoListApp.BLL.DTO.User;
    using ToDoListApp.BLL.Interfaces;
    using ToDoListApp.DAL.Models;

    public record RegisterUserCommand(UserRegisterDTO UserRegister) : IRequest<Result<UserAuthenticationResponseDTO>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserAuthenticationResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Result<UserAuthenticationResponseDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.UserRegister);
            IdentityResult result = await _userManager.CreateAsync(user, request.UserRegister.Password);
            if (!result.Succeeded)
            {
                return Result.Fail("User creation failed");
            }

            List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
        };

            await _userManager.AddClaimsAsync(user, claims);
            JwtSecurityToken token = _tokenService.GenerateToken(new ClaimsIdentity(claims));
            string tokenString = _tokenService.WriteToken(token);
            return Result.Ok(new UserAuthenticationResponseDTO
            {
                Token = tokenString,
                User = _mapper.Map<UserDTO>(user),
            });
        }
    }
}