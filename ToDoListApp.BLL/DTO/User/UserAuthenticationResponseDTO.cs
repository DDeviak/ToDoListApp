namespace ToDoListApp.BLL.DTO.User
{
    public class UserAuthenticationResponseDTO
    {
        public string Token { get; set; } = null!;

        public UserDTO User { get; set; } = null!;
    }
}