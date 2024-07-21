namespace ToDoListApp.BLL.DTO.TaskToDo;
public class TaskToDoDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Deadline { get; set; }

    public Guid TasklistId { get; set; }

    public TaskStatus Status { get; set; }
}