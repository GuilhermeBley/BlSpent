namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.RolePage"/>
/// </summary>
public class PageAndRolePageModel
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
    /// Page name
    /// </summary>
    public string PageName { get; set; } = string.Empty;

    /// <summary>
    /// Role page
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Create date of page
    /// </summary>
    public DateTime PageCreateDate { get; set; }

    /// <summary>
    /// Concurrency stamp, represents last refresh
    /// </summary>
    public Guid ConcurrencyStamp { get; set; }
}