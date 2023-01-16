namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a guest page
/// </summary>
public class RolePage : Entity
{
    /// <summary>
    /// Id
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid PageId { get; }

    /// <summary>
    /// Rule
    /// </summary>
    public string Role { get; }

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; private set; }

    /// <summary>
    /// Private instance
    /// </summary>
    /// <param name="id"><inheritdoc cref="Id" path="/summary"/></param>
    /// <param name="userId"><inheritdoc cref="UserId" path="/summary"/></param>
    /// <param name="pageId"><inheritdoc cref="PageId" path="/summary"/></param>
    /// <param name="createDate"><inheritdoc cref="CreateDate" path="/summary"/></param>
    private RolePage(Guid id, Guid userId, Guid pageId, string role, DateTime createDate)
    {
        Id = id;
        UserId = userId;
        CreateDate = createDate;
        PageId = pageId;
        Role = role;
    }

    
    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var guestPage = obj as RolePage;
        if (guestPage is null)
            return false;

        if (!this.UserId.Equals(guestPage.UserId) ||
            !this.PageId.Equals(guestPage.PageId) ||
            !this.CreateDate.Equals(guestPage.CreateDate))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 954734734;
    }

    /// <summary>
    /// Creates a new <see cref="RolePage"/>
    /// </summary>
    /// <param name="userId"><inheritdoc cref="UserId" path="/summary"/></param>
    /// <param name="pageId"><inheritdoc cref="PageId" path="/summary"/></param>
    /// <param name="role"><inheritdoc cref="Role" path="/summary"/></param>
    /// <returns>new <see cref="RolePage"/></returns>
    /// <exception cref="GenericCoreException"></exception>
    public static RolePage Create(Guid userId, Guid pageId, string role)
    {
        var id = Guid.NewGuid();

        if (userId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid userId. Guid Empty.");

        if (pageId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid idPage. Guid Empty.");

        if (!Security.PageClaim.AvailableRoles.Contains(role))
            throw new GenericCoreException($"Role {role} isn't registred. Try {string.Join(',', Security.PageClaim.AvailableRoles)}.");

        return new RolePage(Guid.NewGuid(), userId, pageId, role, DateTime.Now);
    }

    /// <inheritdoc cref="Create(Guid, Guid, string)" path="*"/>
    public static RolePage CreateReadOnlyRolePage(Guid userId, Guid pageId)
    {
        return Create(userId, pageId, Security.PageClaim.ReadOnly.Value);
    }

    /// <inheritdoc cref="Create(Guid, Guid, string)" path="*"/>
    public static RolePage CreateModifierRolePage(Guid userId, Guid pageId)
    {
        return Create(userId, pageId, Security.PageClaim.Modifier.Value);
    }

    /// <inheritdoc cref="Create(Guid, Guid, string)" path="*"/>
    public static RolePage CreateOwnerRolePage(Guid userId, Guid pageId)
    {
        return Create(userId, pageId, Security.PageClaim.Owner.Value);
    }
}