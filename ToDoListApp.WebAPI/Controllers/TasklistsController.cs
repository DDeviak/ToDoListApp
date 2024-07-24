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

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(TasklistDTO))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put([FromBody] TasklistDTO tasklist)
    {
        return HandleResult(await Mediator.Send(new UpdateTasklistCommand(tasklist)));
    }

    [HttpPatch("{id:guid}")]
    [Consumes(MediaTypeNames.Application.JsonPatch)]
    [ProducesResponseType(200, Type = typeof(TasklistDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<TasklistDTO> patch)
    {
        if (patch is null)
        {
            return BadRequest("Patch document is null.");
        }

        var result = await Mediator.Send(new GetTasklistQuery(id));
        var task = result.ValueOrDefault;
        if (task is null)
        {
            return NotFound("Task not found.");
        }

        patch.ApplyTo(task, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return HandleResult(await Mediator.Send(new UpdateTasklistCommand(task)));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteTasklistCommand(id)));
    }
}
