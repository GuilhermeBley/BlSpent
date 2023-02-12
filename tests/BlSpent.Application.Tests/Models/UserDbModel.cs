using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.User"/>
/// </summary>
[Table("Users")]
public class UserDbModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// Username is same of user email, the username is unique
    /// </summary>
    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [StringLength(BlSpent.Core.Entities.User.MAX_CHAR_USERNAME_AND_EMAIL, MinimumLength = BlSpent.Core.Entities.User.MIN_CHAR_USERNAME_AND_EMAIL)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Email confirmed
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    [StringLength(BlSpent.Core.Entities.User.MAX_CHAR_PHONE)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Is phone number confirmed
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Is two factory enabled
    /// </summary>
    public bool TwoFactoryEnabled { get; set; }

    /// <summary>
    /// Date until lockout
    /// </summary>
    public DateTime? LockOutEnd { get; set; }

    /// <summary>
    /// Is lockout enabled
    /// </summary>
    public bool LockOutEnabled { get; set; }

    /// <summary>
    /// Count of fails access
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    [Required]
    [StringLength(BlSpent.Core.Entities.User.MAX_CHAR_NAME, MinimumLength = BlSpent.Core.Entities.User.MIN_CHAR_NAME)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    [Required]
    [StringLength(BlSpent.Core.Entities.User.MAX_CHAR_LAST_NAME, MinimumLength = BlSpent.Core.Entities.User.MIN_CHAR_LAST_NAME)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Salt
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Salt { get; set; } = string.Empty;
}