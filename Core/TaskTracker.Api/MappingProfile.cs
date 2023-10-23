using AutoMapper;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrerDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<Task, TaskDto>()
                .ForMember(t => t.Status, opt => opt.MapFrom(td => td.Status.StatusName))
                .ForMember(t => t.CreationDate, opt => opt.MapFrom(td => td.CreationDate.ToString("DD/mm/yyyy")))
                .ForMember(t => t.Participants, opt => opt.MapFrom(td => string.Join(", ", td.Participants.Select(p => p.FirstName))))
                .ForMember(t => t.Priority, opt => opt.MapFrom(td => td.Priority.PriorityName));
        }
    }
}
