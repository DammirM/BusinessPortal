using AutoMapper;
using BusinessPortal.DTO_s;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;

namespace BusinessPortal
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, PersonalCreateDTO>().ReverseMap();
            CreateMap<Personal, PersonalUpdateDTO>().ReverseMap();
            CreateMap<RequestType, RequestTypeCreateDTO>().ReverseMap();
            CreateMap<RequestType, RequestTypeUpdateDTO>().ReverseMap();
            CreateMap<RequestCreateDTO, Request>().ReverseMap();
        }
    }
}
