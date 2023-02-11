using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.RolePage"/>
/// </summary>
[Table("RolesPages")]
public class RolePageModel
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }
    public UserModel UserModel { get; set; } = null!;

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid PageId { get; set; }
    public PageModel PageModel { get; set; } = null!;

    /// <summary>
    /// Role page
    /// </summary>
    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; set; }
}