using BlSpent.Application.Model;
using System.Security.Claims;

namespace BlSpent.Application.Repository;

/// <summary>
/// Current claims from user
/// </summary>
public interface IClaimsRepository
{
    /// <summary>
    /// Current session token
    /// </summary>
    /// <returns>string with data or empty</returns>
    Task<string> GetToken();

    /// <summary>
    /// Current claim
    /// </summary>
    /// <returns><see cref="ClaimModel"/> or null if not found.</returns>
    Task<ClaimModel> GetClaimModelOrDefault();

    /// <summary>
    /// Current claims
    /// </summary>
    /// <returns>enumerable of <see cref="Claim"/></returns>
    Task<IEnumerable<Claim>> GetAllClaims();

    /// <summary>
    /// Claim which represents <see cref="BlSpent.Core.Security.PageClaim"/>
    /// </summary>
    /// <returns><see cref="Claim"/> or default if not found</returns>
    Task<Claim> GetPageClaimOrDefault();

    /// <summary>
    /// Claim which represents <see cref="BlSpent.Core.Security.PageClaimId"/>
    /// </summary>
    /// <returns><see cref="Claim"/> or null if not found</returns>
    Task<Claim> GetPageClaimIdOrDefault();
}