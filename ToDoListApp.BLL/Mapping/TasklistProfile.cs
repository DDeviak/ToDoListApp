namespace ToDoListApp.BLL.Mapping;
using ToDoListApp.BLL.DTO.Tasklist;
using ToDoListApp.DAL.Models;
using AutoMapper;
public class TasklistProfile : Profile
{
    public TasklistProfile()
    {
        CreateMap<Tasklist, TasklistCreateDTO>().ReverseMap();
        CreateMap<Tasklist, TasklistDTO>().ReverseMap();
    }
}
