namespace BlSpent.Core.Entities;

public class User : Entity
{
    public const int MAX_COUNT_ACCESS_FAILED = 10;
    public const int MIN_COUNT_ACCESS_FAILED = 0;
    public override Guid Id { get; }
    public string UserName => Email;
    public string Email { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool PhoneNumberConfirmed { get; private set; }
    public bool TwoFactoryEnabled { get; private set; }
    public DateTime? LockOutEnd { get; private set; }
    public bool LockOutEnabled { get; private set; }
    public int AccessFailedCount { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }

    private User(
        Guid id, 
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
        if (Guid.Empty == id)
            throw new GenericCoreException($"{nameof(id)} can't be empty.");

        CheckFields(email, emailConfirmed, phoneNumber, phoneNumberConfirmed, twoFactoryEnabled, 
            lockOutEnd, lockOutEnabled, accessFailedCount, name, lastName);

        return new User(id, email.Trim(), emailConfirmed, phoneNumber, phoneNumberConfirmed, 
            twoFactoryEnabled, lockOutEnd, lockOutEnabled, accessFailedCount, name.Trim(), lastName.Trim());
    }

    private static void CheckFields(
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
        
        if (string.IsNullOrWhiteSpace(email))
            throw new GenericCoreException($"{nameof(email)} is null or empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new GenericCoreException($"{nameof(name)} is null or empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new GenericCoreException($"{nameof(lastName)} is null or empty.");

        if (name.Contains(" "))
            throw new GenericCoreException($"{nameof(name)} must not have spaces.");
    }
}