namespace ToDoListApp.BLL.DTO.Tasklist
{
    public class TasklistDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}