using AutoMapper;

namespace BlSpent.Application.Tests.Profiles;

public class ModelsRepository : Profile
{
    public ModelsRepository()
    {
        CreateMap<Application.Model.UserModel, Models.UserDbModel>().ReverseMap();
        CreateMap<Application.Model.PageModel, Models.PageDbModel>().ReverseMap();
        CreateMap<Application.Model.RolePageModel, Models.RolePageDbModel>().ReverseMap();
        CreateMap<Application.Model.CostModel, Models.CostDbModel>().ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            )
            .ReverseMap()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => new Smartec.Validations.BaseDate(src.EntryBaseDate))
            );
        CreateMap<Application.Model.EarningModel, Models.EarningDbModel>()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            )
            .ReverseMap()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => new Smartec.Validations.BaseDate(src.EntryBaseDate))
            );
        CreateMap<Application.Model.GoalModel, Models.GoalDbModel>().ReverseMap();

        CreateMap<Core.Entities.User, Models.UserDbModel>()
            .ForMember(
                dest => dest.Salt,
                opt => opt.MapFrom(src => src.PasswordSalt)
            );
        CreateMap<Core.Entities.Page, Models.PageDbModel>();
        CreateMap<Core.Entities.RolePage, Models.RolePageDbModel>();
        CreateMap<Core.Entities.Cost, Models.CostDbModel>()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            );
        CreateMap<Core.Entities.Earning, Models.EarningDbModel>()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            );
        CreateMap<Core.Entities.Goal, Models.GoalDbModel>();
    }
}