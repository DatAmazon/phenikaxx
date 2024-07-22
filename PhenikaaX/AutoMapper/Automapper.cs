using AutoMapper;
using PhenikaaX.DTOs;
using PhenikaaX.Entities;

namespace PhenikaaX.MappingProfiles
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<Diagnose, DiagnoseDto>();
        }

    }
}
