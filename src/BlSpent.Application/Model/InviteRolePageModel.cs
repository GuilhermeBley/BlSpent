namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.InviteRolePage"/>
/// </summary>
public class InviteRolePageModel
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email 
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Id user invitation owner
    /// </summary>
    public Guid InvitationOwner { get; set; }

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid PageId { get; set; }

    /// <summary>
    /// Rule
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;
}