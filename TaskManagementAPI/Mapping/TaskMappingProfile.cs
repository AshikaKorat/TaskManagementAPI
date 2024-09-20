using AutoMapper;
using TaskManagementAPI.Core.Models;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            // Mapping between TaskModel and TB_Task
            CreateMap<TB_Task, TaskModel>().ReverseMap();
        }
    }
}
