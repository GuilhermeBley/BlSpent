using BlSpent.Application.Model;
using BlSpent.Core.Exceptions;
using BlSpent.Core.Security;

namespace BlSpent.Application.Security.Internal;

internal static class SecurityChecker
{
    /// <summary>
    /// Check with <see cref="IsLogged(ClaimModel?)"/>
    /// </summary>
    /// <param name="claimModel">claim model</param>
    /// <exception cref="UnauthorizedCoreException"></exception>
    public static void ThrowIfIsntLogged(this ClaimModel? claimModel)
    {
        if (!IsLogged(claimModel))
            throw new UnauthorizedCoreException();
    }

    /// <summary>
    /// Check with <see cref="IsAuthorizedInPage(ClaimModel?)"/>
    /// </summary>
    /// <param name="claimModel">claim model</param>
    /// <exception cref="ForbiddenCoreException"></exception>
    public static void ThrowIfIsntAuthorizedInPage(this ClaimModel? claimModel)
    {
        if (!IsAuthorizedInPage(claimModel))
            throw new ForbiddenCoreException("Page not allowed.");
    }

    /// <summary>
    /// Check with <see cref="CanRead(ClaimModel?)"/>
    /// </summary>
    /// <param name="claimModel">claim model</param>
    /// <exception cref="ForbiddenCoreException"></exception>

    public static void ThrowIfCantRead(this ClaimModel? claimModel)
    {
        if (!CanRead(claimModel))
            throw new ForbiddenCoreException("Read not allowed.");
    }

    /// <summary>
    /// Check with <see cref="CanModify(ClaimModel?)"/>
    /// </summary>
    /// <param name="claimModel">claim model</param>
    /// <exception cref="ForbiddenCoreException"></exception>
    public static void ThrowIfCantModify(this ClaimModel? claimModel)
    {
        if (!CanModify(claimModel))
            throw new ForbiddenCoreException("Modifications not allowed.");
    }

    /// <summary>
    /// Check with <see cref="IsOwner(ClaimModel?)"/>
    /// </summary>
    /// <param name="claimModel">claim model</param>
    /// <exception cref="ForbiddenCoreException"></exception>

    public static void ThrowIfIsntOwner(this ClaimModel? claimModel)
    {
        if (!IsOwner(claimModel))
            throw new ForbiddenCoreException("Only owners can access this resource.");;
    }

    /// <summary>
    /// Checks if is logged
    /// </summary>
    /// <param name="claimModel">claim model to check</param>
    /// <returns>true is logged, otherwise, isn't</returns>
    public static bool IsLogged(this ClaimModel? claimModel)
    {
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
    public static bool IsAuthorizedInPage(this ClaimModel? claimModel)
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
    public static bool CanRead(this ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !IsAuthorizedInPage(claimModel))
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
    public static bool CanModify(this ClaimModel? claimModel)
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
    public static bool IsOwner(this ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !CanModify(claimModel))
            return false;

        if (claimModel.AccessType is not null &&
            claimModel.AccessType.Equals(PageClaim.Owner.Value))
            return true;

        return false;
    }
}