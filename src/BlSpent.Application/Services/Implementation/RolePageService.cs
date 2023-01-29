using BlSpent.Application.Model;
using BlSpent.Application.Security;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;

namespace BlSpent.Application.Services.Implementation;

public class RolePageService : BaseService, IRolePageService
{
    private readonly IUnitOfWork _uoW;
    private readonly IRolePageRepository _rolePageRepository;
    private readonly IPageRepository _pageRepository;

    public RolePageService(
        IUnitOfWork unitOfWork,
        IRolePageRepository rolePageRepository,
        IPageRepository pageRepository,
        ISecurityContext securityContext) : base(securityContext)
    {
        _uoW = unitOfWork;
        _rolePageRepository = rolePageRepository;
        _pageRepository = pageRepository;
    }

    public async Task<RolePageModel?> CurrentOwnerRemove(Guid rolePageId)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel> CurrentOwnerUpdateRoleModifier(RolePageModel rolePageModel)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel> CurrentOwnerUpdateRoleReadOnly(RolePageModel rolePageModel)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel?> GetByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<RolePageModel> GetByPage(Guid pageId)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel> InviteRoleModifier(RolePageModel pageModel)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel> InviteRoleReadOnly(RolePageModel rolePageModel)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel> RemoveByIdOrDefault(Guid rolePageId)
    {
        throw new NotImplementedException();
    }
}