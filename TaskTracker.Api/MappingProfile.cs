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

            CreateMap<Entities.Models.Task, TaskDto>();

            CreateMap<Project, ProjectDto>();

            CreateMap<User, UserDto>();

            CreateMap<User, ParticipantDto>();

            CreateMap<ParticipantDto, User>();

            CreateMap<Project, ProjectForCreationDto>();

            CreateMap<ProjectForCreationDto, Project>();

            CreateMap<TaskForCreationDto, Entities.Models.Task>();
        }
    }
}
