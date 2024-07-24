using Newtonsoft.Json;

namespace ToDoListApp.BLL.DTO.TaskToDo;

public class TaskToDoCreateDTO
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [JsonProperty("todolistId")]
    public Guid TasklistId { get; set; }

    public DateTime Deadline { get; set; }
}
