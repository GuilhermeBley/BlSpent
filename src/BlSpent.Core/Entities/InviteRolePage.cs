namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a guest page
/// </summary>
public class InviteRolePage : Entity
{
    /// <summary>
    /// Id
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// Email 
    /// </summary>
    public string Email { get; }

    public Guid InvitationOwner { get; }

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

    public InviteRolePage(Guid id, string email, Guid invitationOwner, Guid pageId, string role, DateTime createDate)
    {
        Id = id;
        Email = email;
        InvitationOwner = invitationOwner;
        PageId = pageId;
        Role = role;
        CreateDate = createDate;
    }

    

    
    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var inviteRolePage = obj as InviteRolePage;
        if (inviteRolePage is null)
            return false;

        if (!this.Email.Equals(inviteRolePage.Email) ||
            !this.PageId.Equals(inviteRolePage.PageId) ||
            !this.CreateDate.Equals(inviteRolePage.CreateDate) ||
            !this.InvitationOwner.Equals(inviteRolePage.InvitationOwner))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 459864869;
    }

        /// <summary>
    /// Creates a new <see cref="RolePage"/>
    /// </summary>
    /// <param name="email"><inheritdoc cref="Email" path="/summary"/></param>
    /// <param name="pageId"><inheritdoc cref="PageId" path="/summary"/></param>
    /// <param name="role"><inheritdoc cref="Role" path="/summary"/></param>
    /// <returns>new <see cref="RolePage"/></returns>
    /// <exception cref="GenericCoreException"></exception>
    public static InviteRolePage Create(string email, Guid pageId, Guid invitationOwner, string role)
    {
        var id = Guid.NewGuid();

        if (string.IsNullOrEmpty(email) ||
            !Internal.Validation.IsValidEmail(email))
            throw new GenericCoreException("Invalid email.");

        if (invitationOwner.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid invitationOwner. Guid Empty.");

        if (pageId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid idPage. Guid Empty.");

        if (!Security.PageClaim.AvailableRoles.Where(claim => !claim.Equals(Security.PageClaim.Owner))
            .Contains(role))
            throw new GenericCoreException($"Role {role} isn't registred. Try {string.Join(',', Security.PageClaim.AvailableRoles)}.");

        return new InviteRolePage(Guid.NewGuid(), email, invitationOwner, pageId, role, DateTime.Now);
    }
}