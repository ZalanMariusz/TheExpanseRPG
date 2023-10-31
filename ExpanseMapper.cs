using AutoMapper;
using AutoMapper.Internal;
using System;
using TheExpanseRPG.Core.MVVM.ViewModel;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG
{
    public static class ExpanseMapper
    {
        //public static IMapper Mapper { get; set; }
        //public static void InitializeMapper()
        //{
        //    IMapper _mapper;
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<OriginSelectViewModel, CharacterCreationViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<CharacterCreationViewModel,OriginSelectViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<SocialAndBackgroundViewModel, CharacterCreationViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<CharacterCreationViewModel, SocialAndBackgroundViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<CharacterProfessionViewModel, CharacterCreationViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<CharacterCreationViewModel, CharacterProfessionViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<DrivesViewModel, CharacterCreationViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<CharacterCreationViewModel, DrivesViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<AbilityRollViewModel, CharacterCreationViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<CharacterCreationViewModel, AbilityRollViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<AbilityRollViewModel, AllRandomAbilityRollViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<AllRandomAbilityRollViewModel, AbilityRollViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<AbilityRollViewModel, AssignAbilityRollViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<AssignAbilityRollViewModel, AbilityRollViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));

        //        cfg.CreateMap<AbilityRollViewModel, DistributeAbilitiesViewModel>()
        //            .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));
        //        cfg.CreateMap<DistributeAbilitiesViewModel, AbilityRollViewModel>()
        //           .ForMember(dest => dest.CharacterCreationService, act => act.MapFrom(src => src.CharacterCreationService));


        //    });
        //    _mapper = config.CreateMapper();
        //    Mapper = _mapper;
        //}

        //public static object? Map(object source, object destination)
        //{
        //    try
        //    {
        //        return Mapper.Map(source, destination);
        //    }
        //    catch (AutoMapperMappingException)
        //    {
        //        return null;
        //    }
        //}

    }
}
