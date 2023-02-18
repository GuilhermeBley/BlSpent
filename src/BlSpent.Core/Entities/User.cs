namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a user
/// </summary>
public class User : Entity
{
    public const int MAX_COUNT_ACCESS_FAILED = 10;
    public const int MIN_COUNT_ACCESS_FAILED = 0;
    public const int MIN_CHAR_USERNAME_AND_EMAIL = 4;
    public const int MAX_CHAR_USERNAME_AND_EMAIL = 255;
    public const int MIN_CHAR_PHONE = 8;
    public const int MAX_CHAR_PHONE = 15;
    public const int MIN_CHAR_NAME = 1;
    public const int MAX_CHAR_NAME = 100;
    public const int MIN_CHAR_LAST_NAME = 1;
    public const int MAX_CHAR_LAST_NAME = 255;
    public const int MIN_CHAR_PASSWORD = 8;
    public const int MAX_CHAR_PASSWORD = 100;
    public const string ALLOWED_CHAR_PASSWORD = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@$!%*#?& ";
    public const string PATTERN_REGEX_PASSWORD = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,100}$";

    private static GenericCoreException CreateInvalidPasswordCoreException =>
        new GenericCoreException($"Invalid password. Passwords must have at least one letter, one number, one symbol and in total 8 characters of length.");

    /// <summary>
    /// Identifier
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// Username is same of user email, the username is unique
    /// </summary>
    public string UserName => Email;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Email confirmed
    /// </summary>
    public bool EmailConfirmed { get; private set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; private set; }

    /// <summary>
    /// Is phone number confirmed
    /// </summary>
    public bool PhoneNumberConfirmed { get; private set; }

    /// <summary>
    /// Is two factory enabled
    /// </summary>
    public bool TwoFactoryEnabled { get; private set; }

    /// <summary>
    /// Date until lockout
    /// </summary>
    public DateTime? LockOutEnd { get; private set; }

    /// <summary>
    /// Is lockout enabled
    /// </summary>
    public bool LockOutEnabled { get; private set; }

    /// <summary>
    /// Count of fails access
    /// </summary>
    public int AccessFailedCount { get; private set; }

    /// <summary>
    /// First name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Password
    /// </summary>
    public char[] Password { get; private set; }

    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }

    /// <summary>
    /// Private instance
    /// </summary>
    /// <param name="id"><inheritdoc cref="Id" path="/summary"/></param>
    /// <param name="email"><inheritdoc cref="Email" path="/summary"/></param>
    /// <param name="emailConfirmed"><inheritdoc cref="EmailConfirmed" path="/summary"/></param>
    /// <param name="phoneNumber"><inheritdoc cref="PhoneNumber" path="/summary"/></param>
    /// <param name="phoneNumberConfirmed"><inheritdoc cref="PhoneNumberConfirmed" path="/summary"/></param>
    /// <param name="twoFactoryEnabled"><inheritdoc cref="TwoFactoryEnabled" path="/summary"/></param>
    /// <param name="lockOutEnd"><inheritdoc cref="LockOutEnd" path="/summary"/></param>
    /// <param name="lockOutEnabled"><inheritdoc cref="LockOutEnabled" path="/summary"/></param>
    /// <param name="accessFailedCount"><inheritdoc cref="AccessFailedCount" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="lastName"><inheritdoc cref="LastName" path="/summary"/></param>
    private User(
        Guid id, 
        string email, 
        bool emailConfirmed, 
        string? phoneNumber, 
        bool phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName,
        char[] password,
        string passwordHash,
        string passwordSalt)
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
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    
    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var user = obj as User;
        if (user is null)
            return false;

        if (!this.Email.Equals(user.Email) ||
            !this.EmailConfirmed.Equals(user.EmailConfirmed) ||
            this.PhoneNumber != user.PhoneNumber ||
            !this.Password.Equals(user.Password) ||
            !this.PhoneNumberConfirmed.Equals(user.PhoneNumberConfirmed) ||
            !this.TwoFactoryEnabled.Equals(user.TwoFactoryEnabled) ||
            !this.LockOutEnd.Equals(user.LockOutEnd) ||
            !this.LockOutEnabled.Equals(user.LockOutEnabled) ||
            !this.AccessFailedCount.Equals(user.AccessFailedCount) ||
            !this.Name.Equals(user.Name) ||
            !this.LastName.Equals(user.LastName))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 45864957;
    }

    /// <summary>
    /// Creates a new instance of <see cref="User"/>
    /// </summary>
    /// <param name="email"><inheritdoc cref="Email" path="/summary"/></param>
    /// <param name="emailConfirmed"><inheritdoc cref="EmailConfirmed" path="/summary"/></param>
    /// <param name="phoneNumber"><inheritdoc cref="PhoneNumber" path="/summary"/></param>
    /// <param name="phoneNumberConfirmed"><inheritdoc cref="PhoneNumberConfirmed" path="/summary"/></param>
    /// <param name="twoFactoryEnabled"><inheritdoc cref="TwoFactoryEnabled" path="/summary"/></param>
    /// <param name="lockOutEnd"><inheritdoc cref="LockOutEnd" path="/summary"/></param>
    /// <param name="lockOutEnabled"><inheritdoc cref="LockOutEnabled" path="/summary"/></param>
    /// <param name="accessFailedCount"><inheritdoc cref="AccessFailedCount" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="lastName"><inheritdoc cref="LastName" path="/summary"/></param>
    /// <param name="password"><inheritdoc cref="Password" path="/summary"/></param>
    /// <returns>new <see cref="User"/></returns>
    public static User Create(
        string email, 
        bool emailConfirmed, 
        string? phoneNumber, 
        bool phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName,
        char[] password,
        string passwordHash,
        string passwordSalt)
    {
        var id = Guid.NewGuid();

        CheckFields(email, emailConfirmed, phoneNumber, phoneNumberConfirmed, twoFactoryEnabled, 
            lockOutEnd, lockOutEnabled, accessFailedCount, name, lastName);

        if (!IsValidPassword(password))
            throw CreateInvalidPasswordCoreException;

        return new User(id, email.Trim(), emailConfirmed, phoneNumber, phoneNumberConfirmed, 
            twoFactoryEnabled, lockOutEnd, lockOutEnabled, accessFailedCount, name.Trim(), lastName.Trim(), 
            password, passwordHash, passwordSalt);
    }

    /// <summary>
    /// Creates a new instance of <see cref="User"/>
    /// </summary>
    /// <param name="email"><inheritdoc cref="Email" path="/summary"/></param>
    /// <param name="emailConfirmed"><inheritdoc cref="EmailConfirmed" path="/summary"/></param>
    /// <param name="phoneNumber"><inheritdoc cref="PhoneNumber" path="/summary"/></param>
    /// <param name="phoneNumberConfirmed"><inheritdoc cref="PhoneNumberConfirmed" path="/summary"/></param>
    /// <param name="twoFactoryEnabled"><inheritdoc cref="TwoFactoryEnabled" path="/summary"/></param>
    /// <param name="lockOutEnd"><inheritdoc cref="LockOutEnd" path="/summary"/></param>
    /// <param name="lockOutEnabled"><inheritdoc cref="LockOutEnabled" path="/summary"/></param>
    /// <param name="accessFailedCount"><inheritdoc cref="AccessFailedCount" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="lastName"><inheritdoc cref="LastName" path="/summary"/></param>
    /// <param name="password"><inheritdoc cref="Password" path="/summary"/></param>
    /// <returns>new <see cref="User"/></returns>
    public static User CreateWithHashAndSalt(
        string email, 
        bool emailConfirmed, 
        string? phoneNumber, 
        bool phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName,
        string passwordHash,
        string passwordSalt)
    {
        var id = Guid.NewGuid();

        CheckFields(email, emailConfirmed, phoneNumber, phoneNumberConfirmed, twoFactoryEnabled, 
            lockOutEnd, lockOutEnabled, accessFailedCount, name, lastName);

        if (string.IsNullOrWhiteSpace(passwordHash) ||
            string.IsNullOrWhiteSpace(passwordSalt))
            throw CreateInvalidPasswordCoreException;

        return new User(id, email.Trim(), emailConfirmed, phoneNumber, phoneNumberConfirmed, 
            twoFactoryEnabled, lockOutEnd, lockOutEnabled, accessFailedCount, name.Trim(), lastName.Trim(), 
            new char[0], passwordHash, passwordSalt);
    }

    /// <inheritdoc cref="Create(string, bool, string?, bool, bool, DateTime?, bool, int, string, string, char[])" path="*"/>
    public static User Create(
        string email, 
        bool emailConfirmed, 
        string? phoneNumber, 
        bool phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName,
        string password,
        string passwordHash,
        string passwordSalt)
    {
        return Create(email, emailConfirmed, phoneNumber, phoneNumberConfirmed, 
            twoFactoryEnabled, lockOutEnd, lockOutEnabled, accessFailedCount, name, lastName, 
            password.ToCharArray(), passwordHash, passwordSalt);
    }

    private static void CheckFields(
        string email, 
        bool emailConfirmed, 
        string? phoneNumber, 
        bool? phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName)
    {
        if (accessFailedCount > MAX_COUNT_ACCESS_FAILED 
            || accessFailedCount < MIN_COUNT_ACCESS_FAILED)
            throw new GenericCoreException($"{nameof(accessFailedCount)} must be in range between '{MIN_COUNT_ACCESS_FAILED}' to '{MAX_COUNT_ACCESS_FAILED}'.");
        
        if (string.IsNullOrWhiteSpace(email) ||
            !Internal.Validation.IsValidEmail(email))
            throw new GenericCoreException($"{nameof(email)} is empty or invalid.");

        if (string.IsNullOrWhiteSpace(name))
            throw new GenericCoreException($"{nameof(name)} is null or empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new GenericCoreException($"{nameof(lastName)} is null or empty.");

        if (name.Contains(" "))
            throw new GenericCoreException($"{nameof(name)} must not have spaces.");

        if (email.Length < MIN_CHAR_USERNAME_AND_EMAIL ||
            email.Length > MAX_CHAR_USERNAME_AND_EMAIL)
            throw new GenericCoreException($"{nameof(email)} must have a length between {MIN_CHAR_USERNAME_AND_EMAIL} and {MAX_CHAR_USERNAME_AND_EMAIL}.");

        if (phoneNumber is not null &&
            (phoneNumber.Length < MIN_CHAR_PHONE ||
            phoneNumber.Length > MAX_CHAR_PHONE ||
            phoneNumber.All(char.IsNumber)))
            throw new GenericCoreException($"{nameof(phoneNumber)} must have only numbers, and a length between {MIN_CHAR_PHONE} and {MAX_CHAR_PHONE}.");
        
        if (name.Length < MIN_CHAR_NAME ||
            name.Length > MAX_CHAR_NAME)
            throw new GenericCoreException($"{nameof(name)} must have a length between {MIN_CHAR_NAME} and {MAX_CHAR_NAME}.");
        
        if (lastName.Length < MIN_CHAR_LAST_NAME ||
            lastName.Length > MAX_CHAR_LAST_NAME)
            throw new GenericCoreException($"{nameof(lastName)} must have a length between {MIN_CHAR_LAST_NAME} and {MAX_CHAR_LAST_NAME}.");
    }
    
    /// <summary>
    /// Check if is valid password, if invalid, return false.
    /// </summary>
    /// <param name="password">password to check</param>
    public static bool IsValidPassword(params char[] password)
    {
        return IsValidPassword(new string(password));
    }

    /// <summary>
    /// Check if is valid password, if invalid, return false.
    /// </summary>
    /// <param name="password">password to check</param>
    public static bool IsValidPassword(string password)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(password, PATTERN_REGEX_PASSWORD))
            return true;

        if (password.Any(c => !ALLOWED_CHAR_PASSWORD.Contains(c)))
            throw new GenericCoreException($"Invalid character found in password. Is allowed only '{ALLOWED_CHAR_PASSWORD}'.");

        return false;
    }

    /// <summary>
    /// Check if is valid password, if invalid, throw <see cref="GenericCoreException"/>
    /// </summary>
    /// <param name="password">password to check</param>
    /// <exception cref="GenericCoreException"></exception>
    public static void ThrowIfIsInvalidPassword(params char[] password)
    {
        ThrowIfIsInvalidPassword(new string(password));
    }
    
    /// <summary>
    /// Check if is valid password, if invalid, throw <see cref="GenericCoreException"/>
    /// </summary>
    /// <param name="password">password to check</param>
    /// <exception cref="GenericCoreException"></exception>
    public static void ThrowIfIsInvalidPassword(string password)
    {
        if (!IsValidPassword(password))
            throw CreateInvalidPasswordCoreException;
    }
}