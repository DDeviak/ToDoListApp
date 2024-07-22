namespace ToDoListApp.WebAPI.Controllers;

using ToDoListApp.BLL.MediatR.Tasklist;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.BLL.DTO.Tasklist;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

[Authorize]
[ProducesResponseType(401)]
public class TasklistsController : BaseApiController
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200, Type = typeof(TasklistDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetTasklistQuery(id)));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(200, Type = typeof(List<TasklistDTO>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByUser([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetTasklistsByUserQuery(id)));
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(TasklistDTO))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] TasklistCreateDTO tasklist)
    {
        return HandleResult(await Mediator.Send(new CreateTasklistCommand(tasklist)));
    }

    [HttpPatch("{id:guid}")]
    [Consumes(MediaTypeNames.Application.JsonPatch)]
    [ProducesResponseType(200, Type = typeof(TasklistDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<TasklistDTO> patch)
    {
        var result = await Mediator.Send(new GetTasklistQuery(id));
        var tasklist = result.Value;
        patch.ApplyTo(tasklist, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await Mediator.Send(new UpdateTasklistCommand(tasklist)));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteTasklistCommand(id)));
    }
}
