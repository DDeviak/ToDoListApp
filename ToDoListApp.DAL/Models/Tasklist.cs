namespace ToDoListApp.DAL.Models
{
    public class Tasklist
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<TaskToDo> Tasks { get; set; } = null!;
    }
}