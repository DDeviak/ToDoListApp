namespace ToDoListApp.BLL.DTO.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}