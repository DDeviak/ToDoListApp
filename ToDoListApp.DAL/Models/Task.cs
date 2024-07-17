namespace ToDoListApp.DAL.Models;
public class Task
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public int TasklistId { get; set; }
    public Tasklist? Tasklist { get; set; }
    public TaskStatus Status { get; set; }
}