using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.BLL.DTO.User;
using ToDoListApp.BLL.MediatR.User;

namespace ToDoListApp.WebAPI.Controllers;

public class UsersController : BaseApiController
{
    [HttpPost("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(UserAuthenticationResponseDTO))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
    {
        return HandleResult(await Mediator.Send(new RegisterUserCommand(user)));
    }

    [HttpPost("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(UserAuthenticationResponseDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
    {
        return HandleResult(await Mediator.Send(new LoginUserQuery(user)));
    }
}