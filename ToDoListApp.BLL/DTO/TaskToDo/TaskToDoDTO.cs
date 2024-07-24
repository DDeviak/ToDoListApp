using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ToDoListApp.DAL.Enums;

namespace ToDoListApp.BLL.DTO.TaskToDo;
public class TaskToDoDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Deadline { get; set; }

    [JsonProperty("todolistId")]
    public Guid TasklistId { get; set; }

    [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter))]
    public ToDoListTaskStatus Status { get; set; }
}