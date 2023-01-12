namespace BlSpent.Core.Entities;

public class User : Entity
{
    public const int MAX_COUNT_ACCESS_FAILED = 10;
    public const int MIN_COUNT_ACCESS_FAILED = 0;
    public override Guid Id { get; }
    public string UserName { get; }
    public string Email { get; }
    public bool EmailConfirmed { get; }
    public string PhoneNumber { get; }
    public bool PhoneNumberConfirmed { get; }
    public bool TwoFactoryEnabled { get; }
    public DateTime? LockOutEnd { get; }
    public bool LockOutEnabled { get; }
    public int AccessFailedCount { get; }
    public string Name { get; }
    public string LastName { get; }

    private User(
        Guid id, 
        string userName, 
        string email, 
        bool emailConfirmed, 
        string phoneNumber, 
        bool phoneNumberConfirmed, 
        bool twoFactoryEnabled, 
        DateTime? lockOutEnd, 
        bool lockOutEnabled, 
        int accessFailedCount, 
        string name, 
        string lastName)
    {
        Id = id;
        UserName = userName;
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
    }

    public static User Create(
        Guid id, 
        string userName, 
        string email, 
        bool emailConfirmed, 
        string phoneNumber, 
        bool phoneNumberConfirmed, 
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

        if (string.IsNullOrWhiteSpace(userName))
            throw new GenericCoreException($"{nameof(userName)} is null or empty.");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new GenericCoreException($"{nameof(email)} is null or empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new GenericCoreException($"{nameof(name)} is null or empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new GenericCoreException($"{nameof(lastName)} is null or empty.");

        if (name.Contains(" "))
            throw new GenericCoreException($"{nameof(name)} must not have spaces.");

        return new User(id, userName, email, emailConfirmed, phoneNumber, phoneNumberConfirmed, 
            twoFactoryEnabled, lockOutEnd, lockOutEnabled, accessFailedCount, name, lastName);
    }
}