using BlSpent.Application.Model;
using BlSpent.Core.Exceptions;
using BlSpent.Core.Security;

namespace BlSpent.Application.Security.Internal;

internal static class SecurityChecker
{
    public static void ThrowIfIsntLogged(this ClaimModel? claimModel)
    {
        if (!IsLogged(claimModel))
            throw new UnauthorizedCoreException();
    }

    public static void ThrowIfIsntAuthorizedInPage(this ClaimModel? claimModel)
    {
        if (!IsAuthorizedInPage(claimModel))
            throw new ForbiddenCoreException("Page not allowed.");
    }

    public static void ThrowIfCantRead(this ClaimModel? claimModel)
    {
        if (!CanRead(claimModel))
            throw new ForbiddenCoreException("Read not allowed.");
    }

    public static void ThrowIfCantModify(this ClaimModel? claimModel)
    {
        if (!CanModify(claimModel))
            throw new ForbiddenCoreException("Modifications not allowed.");
    }

    public static void ThrowIfIsntOwner(this ClaimModel? claimModel)
    {
        if (!IsOwner(claimModel))
            throw new ForbiddenCoreException("Only owners can access this resource.");;
    }


    public static bool IsLogged(this ClaimModel? claimModel)
    {
        if (claimModel is null ||
            claimModel.Equals(ClaimModel.Default) ||
            claimModel.UserId is null)
            return false;

        return true;
    }

    public static bool IsAuthorizedInPage(this ClaimModel? claimModel)
    {
        if (claimModel is null ||
            !IsLogged(claimModel))
            return false;
        
        if (claimModel.PageId is null)
            return false;

        return true;
    }

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