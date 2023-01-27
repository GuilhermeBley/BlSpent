using BlSpent.Application.Security;
using BlSpent.Application.Security.Internal;

namespace BlSpent.Application.Services.Implementation;

public abstract class BaseService
{
    protected readonly ISecurityContext _securityContext;
    internal readonly ClaimModelChecker _securityChecker;
    public BaseService(ISecurityContext securityContext)
    {
        _securityContext = securityContext;
        _securityChecker = new ClaimModelChecker(_securityContext);
    }
}