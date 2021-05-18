using AutoMapper;
using Boleyn.Database.Entities.Authors;
using mediator.Activities.Salutations;

namespace mediator
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSalutationCommand, Salutation>()
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.FullWord, opt => opt.MapFrom(src => src.FullWord));
        }
    }
}