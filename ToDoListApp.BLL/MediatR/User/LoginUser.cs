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

    public record LoginUserCommand(UserLoginDTO UserLogin) : IRequest<Result<UserAuthenticationResponseDTO>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserAuthenticationResponseDTO>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Result<UserAuthenticationResponseDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.UserLogin.Email);
            if (user == null)
            {
                return Result.Fail("User not found");
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.UserLogin.Password);
            if (!isPasswordValid)
            {
                return Result.Fail("Invalid password");
            }

            List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
        };

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