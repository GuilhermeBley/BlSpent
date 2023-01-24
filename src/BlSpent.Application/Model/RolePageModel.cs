namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.RolePage"/>
/// </summary>
public class RolePageModel
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid PageId { get; set; }

    /// <summary>
    /// Role page
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; set; }
}