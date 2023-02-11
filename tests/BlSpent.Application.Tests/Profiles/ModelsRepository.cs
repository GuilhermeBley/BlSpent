using AutoMapper;

namespace BlSpent.Application.Tests.Profiles;

public class ModelsRepository : Profile
{
    public ModelsRepository()
    {
        CreateMap<Application.Model.UserModel, Models.UserModel>().ReverseMap();
        CreateMap<Application.Model.PageModel, Models.PageModel>().ReverseMap();
        CreateMap<Application.Model.RolePageModel, Models.RolePageModel>().ReverseMap();
        CreateMap<Application.Model.CostModel, Models.CostModel>().ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            )
            .ReverseMap()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => new Smartec.Validations.BaseDate(src.EntryBaseDate))
            );
        CreateMap<Application.Model.EarningModel, Models.EarningModel>()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => src.EntryBaseDate.ToDateTime())
            )
            .ReverseMap()
            .ForMember(
                dest => dest.EntryBaseDate,
                opt => opt.MapFrom(src => new Smartec.Validations.BaseDate(src.EntryBaseDate))
            );
        CreateMap<Application.Model.GoalModel, Models.GoalModel>().ReverseMap();
    }
}