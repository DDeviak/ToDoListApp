namespace ToDoListApp.DAL.Models
{
    public class Tasklist
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<TaskToDo>? Tasks { get; set; }
    }
}