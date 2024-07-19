namespace ToDoListApp.DAL.Models;
public class TaskToDo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public Guid TasklistId { get; set; }
    public Tasklist Tasklist { get; set; } = null!;
    public TaskStatus Status { get; set; }
}