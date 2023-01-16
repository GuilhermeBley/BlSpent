namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.User"/>
/// </summary>
public class UserModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Username is same of user email, the username is unique
    /// </summary>
    public string UserName => Email;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Email confirmed
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public char[] Password { get; set; } = new char[0];

    /// <summary>
    /// Password
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Salt
    /// </summary>
    public string Salt { get; set; } = string.Empty;
    
    public UserModel(Guid id, string email, bool emailConfirmed, string? phoneNumber, bool phoneNumberConfirmed, bool twoFactoryEnabled, DateTime? lockOutEnd, bool lockOutEnabled, int accessFailedCount, string name, string lastName, char[] password)
    {
        Id = id;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactoryEnabled = twoFactoryEnabled;
        LockOutEnd = lockOutEnd;
        LockOutEnabled = lockOutEnabled;
        AccessFailedCount = accessFailedCount;
        Name = name;
        LastName = lastName;
        Password = password;
    }
}