namespace BlSpent.Application.Mappings;

internal static class Mapper
{
    public static Core.Entities.Cost Map (Model.CostModel model)
    {
        return Core.Entities.Cost.Create(model.CostDate, model.Value, model.PageId);
    }

    public static Core.Entities.Earning Map (Model.EarningModel model)
    {
        return Core.Entities.Earning.Create(model.EarnDate, model.Value, model.PageId);
    }

    public static Model.RolePageModel MapToRolePage (Model.RoleUserPageModel model)
    {
        return new Model.RolePageModel
        {
            CreateDate = model.CreateDate,
            Id = model.Id,
            PageId = model.PageId,
            Role = model.Role,
            UserId = model.UserId
        };
    }

    public static Core.Entities.RolePage Map (Model.RoleUserPageModel model)
    {
        return Core.Entities.RolePage.Create(model.UserId, model.PageId, model.Role);
    }

    public static Core.Entities.Goal Map (Model.GoalModel model)
    {
        return Core.Entities.Goal.Create(model.TargetDate, model.Value, model.PageId);
    }

    public static Core.Entities.Page Map (Model.PageModel model)
    {
        return Core.Entities.Page.Create(model.PageName);
    }

    public static Core.Entities.RolePage Map (Model.RolePageModel model)
    {
        return Core.Entities.RolePage.Create(model.UserId, model.PageId, model.Role);
    }

    public static Core.Entities.User Map (Model.UserModel model)
    {
        return Core.Entities.User.Create(model.Email, model.EmailConfirmed, 
            model.PhoneNumber, model.PhoneNumberConfirmed, model.TwoFactoryEnabled, 
            model.LockOutEnd, model.LockOutEnabled, model.AccessFailedCount, model.Name, 
            model.LastName, model.Password, model.PasswordHash, model.Salt);
    }
}