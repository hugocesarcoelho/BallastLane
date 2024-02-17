using AutoMapper;
using BallastLane.ApplicationService.Dto;
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
        }
    }
}