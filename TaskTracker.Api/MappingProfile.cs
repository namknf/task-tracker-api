using AutoMapper;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrerDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<Entities.Models.Task, TaskDto>()
                .ForMember(t => t.Status, opt => opt.MapFrom(td => td.Status.StatusName))
                .ForMember(t => t.Priority, opt => opt.MapFrom(td => td.Priority.PriorityName));

            CreateMap<Project, ProjectDto>();

            CreateMap<User, UserDto>();

            CreateMap<User, ParticipantDto>();
        }
    }
}
