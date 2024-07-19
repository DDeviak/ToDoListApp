namespace ToDoListApp.BLL.DTO.TaskToDo;

public class TaskToDoCreateDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid TasklistId { get; set; }
    public DateTime Deadline { get; set; }
}
