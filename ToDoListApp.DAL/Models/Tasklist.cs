namespace ToDoListApp.DAL.Models
{
    public class Tasklist
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<TaskToDo>? Tasks { get; set; }
    }
}