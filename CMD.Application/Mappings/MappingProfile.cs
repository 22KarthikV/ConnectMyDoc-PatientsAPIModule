using AutoMapper;
using CMD.Application.DTOs;
using CMD.Domain.Entities;
using CMD.Application.DTOs;
using CMD.Domain.Entities;

namespace CMD.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<PatientAddress, PatientAddressDto>().ReverseMap();
            CreateMap<HealthCondition, HealthConditionDto>().ReverseMap();
        }
    }
}