namespace ToDoListApp.WebAPI.Controllers;

using ToDoListApp.BLL.MediatR.Tasklist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using ToDoListApp.BLL.MediatR.TaskToDo;
using ToDoListApp.BLL.DTO.TaskToDo;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class TasksController : BaseApiController
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200, Type = typeof(TaskToDoDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetTaskToDoQuery(id)));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(200, Type = typeof(List<TaskToDoDTO>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByUser([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetTaskToDoByTasklistQuery(id)));
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TaskToDoDTO))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] TaskToDoCreateDTO task)
    {
        return HandleResult(await Mediator.Send(new CreateTaskToDoCommand(task)));
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(200, Type = typeof(TaskToDoDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<TaskToDoDTO> patch)
    {
        var result = await Mediator.Send(new GetTaskToDoQuery(id));
        var task = result.Value;
        patch.ApplyTo(task, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await Mediator.Send(new UpdateTaskToDoCommand(task)));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteTaskToDoCommand(id)));
    }
}
