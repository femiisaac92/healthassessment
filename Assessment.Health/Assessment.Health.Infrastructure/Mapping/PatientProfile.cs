using Assessment.Health.Domain.Entities;
using Assessment.Health.Infrastructure.ViewModel;
using AutoMapper;

namespace Assessment.Health.Infrastructure.Mapping
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientModel, Patient>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();
        }
    }
}
