using BlSpent.Application.Model;

namespace BlSpent.Application.Security;

public interface ISecurityContext
{
    Task<ClaimModel?> GetCurrentClaim();
}