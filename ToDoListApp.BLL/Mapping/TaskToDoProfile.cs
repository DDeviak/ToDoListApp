using ToDoListApp.BLL.DTO.TaskToDo;
using ToDoListApp.DAL.Models;
using AutoMapper;

namespace ToDoListApp.BLL.Mapping
{
    public class TaskToDoProfile : Profile
    {
        public TaskToDoProfile()
        {
            CreateMap<TaskToDo, TaskToDoCreateDTO>().ReverseMap();
            CreateMap<TaskToDo, TaskToDoDTO>().ReverseMap();
        }
    }
}