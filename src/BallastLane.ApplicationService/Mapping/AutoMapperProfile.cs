using AutoMapper;
using BallastLane.ApplicationService.Dto.Application;
using BallastLane.ApplicationService.Dto.User;
using BallastLane.Domain.Model;

namespace BallastLane.ApplicationService.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationCreateInputDto, Application>();
            CreateMap<Application, ApplicationOutputDto>();
            CreateMap<ApplicationUpdateInputDto, Application>();

            CreateMap<UserCreateInputDto, User>();
            CreateMap<User, UserOutputDto>();
            CreateMap<UserUpdateInputDto, User>();
        }
    }
}