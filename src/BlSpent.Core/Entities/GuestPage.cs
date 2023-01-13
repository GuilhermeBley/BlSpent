namespace BlSpent.Core.Entities;

public class GuestPage : Entity
{
    public override Guid Id { get; }
    public Guid UserId { get; }
    public Guid IdPage { get; }
    public DateTime CreateDate { get; private set; }

    private GuestPage(Guid id, Guid userId, Guid idPage, DateTime createDate)
    {
        Id = id;
        UserId = userId;
        CreateDate = createDate;
        IdPage = idPage;
    }

    public static GuestPage Create(Guid userId, Guid idPage)
    {
        var id = Guid.NewGuid();

        if (userId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid userId. Guid Empty.");

        if (idPage.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid idPage. Guid Empty.");

        return new GuestPage(Guid.NewGuid(), userId, idPage, DateTime.Now);
    }
}