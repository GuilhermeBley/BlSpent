namespace BlSpent.Application.Model;

/// <summary>
/// Model of query
/// </summary>
public class RoleUserPageModel
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
    /// First name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; set; }
}