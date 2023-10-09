using AutoMapper;
using BusinessPortal.DTO_s;
using BusinessPortal.Models;

namespace BusinessPortal
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, PersonalCreateDTO>().ReverseMap();
        }
    }
}
