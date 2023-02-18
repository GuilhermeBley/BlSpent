using BlSpent.Application.Model;
using BlSpent.Core.Exceptions;
using BlSpent.Core.Security;

namespace BlSpent.Application.Security.Internal;

internal class ClaimModelChecker
{
    private readonly ISecurityContext _securityContext;

    public ClaimModelChecker(ISecurityContext securityContext)
    {
        _securityContext = securityContext;
    }

    /// <summary>
    /// Check with <see cref="IsLogged(ClaimModel?)"/>
    /// </summary>
    /// <exception cref="UnauthorizedCoreException"></exception>
    public async Task ThrowIfIsntLogged()
    {
        if (!IsLogged(await _securityContext.GetCurrentClaim()))
            throw new UnauthorizedCoreException();
    }

    /// <summary>
    /// Check with <see cref="IsAuthorizedInPage(ClaimModel?)"/>
    /// </summary>
    /// <exception cref="ForbiddenCoreException"></exception>
    public async Task ThrowIfIsntAuthorizedInPage()
    {
        if (!IsAuthorizedInPage(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Page not allowed.");
    }

    /// <summary>
    /// Check with <see cref="CanRead(ClaimModel?)"/>
    /// </summary>
    /// <exception cref="ForbiddenCoreException"></exception>

    public async Task ThrowIfCantRead()
    {
        if (!CanRead(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Read not allowed.");
    }

    /// <summary>
    /// Check with <see cref="CanModify(ClaimModel?)"/>
    /// </summary>
    /// <exception cref="ForbiddenCoreException"></exception>
    public async Task ThrowIfCantModify()
    {
        if (!CanModify(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Modifications not allowed.");
    }

    /// <summary>
    /// Check with <see cref="IsOwner(ClaimModel?)"/>
    /// </summary>
    /// <exception cref="ForbiddenCoreException"></exception>

    public async Task ThrowIfIsntOwner()
    {
        if (!IsOwner(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Only owners can access this resource.");
    }

    public async Task ThrowIfIsntNotRememberPassword()
    {
        if (!IsNotRememberPassword(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Only requests to change password are permitted.");
    }

    public async Task ThrowIfIsntInvitation()
    {
        if (!IsInvite(await _securityContext.GetCurrentClaim()))
            throw new ForbiddenCoreException("Only invites are permitted.");
    }

    private static bool IsExpired(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            claimModel.Expires is null ||
            claimModel.Expires < DateTime.Now)
            return true;

        return false;
    }

    /// <summary>
    /// Checks if is logged
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true is logged, otherwise, isn't</returns>
    private static bool IsLogged(ClaimModel? claimModel)
    {
        if (IsExpired(claimModel))
            return false;
            
        if (claimModel is null ||
            claimModel.Equals(ClaimModel.Default) ||
            claimModel.UserId is null)
            return false;

        return true;
    }

    /// <summary>
    /// Checks if is logged and if is authorized in page
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true is authorized in page, otherwise, isn't</returns>
    private static bool IsAuthorizedInPage(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !IsLogged(claimModel))
            return false;
        
        if (claimModel.PageId is null)
            return false;

        return true;
    }

    /// <summary>
    /// Checks if is authorized in page and if can read it
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true can read page, otherwise, can't</returns>
    private static bool CanRead(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            IsInvite(claimModel))
            return false;

        if (claimModel.AccessType is null ||
            !claimModel.AccessType.Equals(PageClaim.ReadOnly.Value) ||
            !claimModel.AccessType.Equals(PageClaim.Modifier.Value) ||
            !claimModel.AccessType.Equals(PageClaim.Owner.Value))
            return false;

        return true;
    }

    /// <summary>
    /// Checks if can read page and if can modify it
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true can modify page, otherwise, can't</returns>
    private static bool CanModify(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !CanRead(claimModel))
            return false;

        if (claimModel.AccessType is null ||
            !claimModel.AccessType.Equals(PageClaim.Modifier.Value) ||
            !claimModel.AccessType.Equals(PageClaim.Owner.Value))
            return false;

        return true;
    }

    /// <summary>
    /// Checks if is owner of the page
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true is owner, otherwise, isn't</returns>
    private static bool IsOwner(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !CanModify(claimModel))
            return false;

        if (claimModel.AccessType is not null &&
            claimModel.AccessType.Equals(PageClaim.Owner.Value))
            return true;

        return false;
    }

    /// <summary>
    /// Checks if user contains invitation
    /// </summary>
    private static bool IsInvite(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !IsAuthorizedInPage(claimModel))
            return false;

        if (claimModel.IsInvite)
            return true;

        return false;
    }

    /// <summary>
    /// Checks if user request to change not remembered password
    /// </summary>
    private static bool IsNotRememberPassword(ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !IsAuthorizedInPage(claimModel))
            return false;

        if (claimModel.IsNotRememberPassword)
            return true;

        return false;
    }
}