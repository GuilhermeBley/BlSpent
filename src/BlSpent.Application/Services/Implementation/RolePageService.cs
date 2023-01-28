using BlSpent.Application.Model;
using BlSpent.Application.Security;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;

namespace BlSpent.Application.Services.Implementation;

public class RolePageService : BaseService, IRolePageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRolePageRepository _rolePageRepository;

    public RolePageService(
        IUnitOfWork unitOfWork,
        IRolePageRepository rolePageRepository,
        ISecurityContext securityContext) : base(securityContext)
    {
        _unitOfWork = unitOfWork;
        _rolePageRepository = rolePageRepository;
    }

    public Task<RolePageModel?> AddOrUpdateRoleModifier(RolePageModel pageModel)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel?> AddOrUpdateRoleReadOnly(RolePageModel pageModel)
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

    public Task<RolePageModel?> RemoveByIdOrDefault(Guid pageId)
    {
        throw new NotImplementedException();
    }

    public Task<RolePageModel?> UpdateByIdOrDefault(Guid pageId, RolePageModel pageModel)
    {
        throw new NotImplementedException();
    }
}