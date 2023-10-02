using AutoMapper;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrerDto, User>();
        }
    }
}
