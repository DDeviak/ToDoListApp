namespace ToDoListApp.BLL.Mapping;
using ToDoListApp.BLL.DTO.User;
using ToDoListApp.DAL.Models;
using AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegisterDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}