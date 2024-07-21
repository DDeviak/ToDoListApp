using Microsoft.AspNetCore.Mvc;
using ToDoListApp.BLL.DTO.User;
using ToDoListApp.BLL.MediatR.User;

namespace ToDoListApp.WebAPI.Controllers;

public class UsersController : BaseApiController
{
    [HttpPost("register")]
    [ProducesResponseType(200, Type = typeof(UserDTO))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
    {
        return HandleResult(await Mediator.Send(new RegisterUserCommand(user)));
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(UserDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
    {
        return HandleResult(await Mediator.Send(new LoginUserQuery(user)));
    }
}