namespace ToDoListApp.BLL.DTO.Tasklist
{
    public class TasklistCreateDTO
    {
        public string Title { get; set; } = null!;

        public Guid UserId { get; set; }
    }
}