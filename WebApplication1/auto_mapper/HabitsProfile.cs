using AutoMapper;
using WebApplication1.model;
using WebApplication1.model.dto;

public class HabitsProfile : Profile
{
    public HabitsProfile()
    {
        CreateMap<Habits, HabitsDto>()
            .Include<GoodHabits, HabitsDto>()
            .Include<BadHabits, HabitsDto>();

        CreateMap<GoodHabits, HabitsDto>()
            .ForMember(dest => dest.Reward, opt => opt.MapFrom(src => src.Reward));

        CreateMap<BadHabits, HabitsDto>()
            .ForMember(dest => dest.Penalty, opt => opt.MapFrom(src => src.Penalty));

        CreateMap<HabitsDto, GoodHabits>()
            .ForMember(dest => dest.Reward, opt => opt.MapFrom(src => src.Reward))
            .AfterMap((src, dest) =>
            {
                if (src.Discriminator != "GoodHabits")
                    throw new InvalidOperationException("Invalid habit type.");
            });

        CreateMap<HabitsDto, BadHabits>()
            .ForMember(dest => dest.Penalty, opt => opt.MapFrom(src => src.Penalty))
            .AfterMap((src, dest) =>
            {
                if (src.Discriminator != "BadHabits")
                    throw new InvalidOperationException("Invalid habit type.");
            });
        CreateMap<HabitsDto, GoodHabits>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Reward, opt => opt.MapFrom(src => src.Reward));

        CreateMap<HabitsDto, BadHabits>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Penalty, opt => opt.MapFrom(src => src.Penalty));
    }
}