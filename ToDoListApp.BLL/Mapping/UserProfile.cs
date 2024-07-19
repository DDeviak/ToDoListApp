namespace ToDoListApp.BLL.Mapping;
using ToDoListApp.BLL.DTO.User;
using ToDoListApp.DAL.Models;
using AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserCreateDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}