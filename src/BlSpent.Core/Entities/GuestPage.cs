namespace BlSpent.Core.Entities;

public class GuestPage : Entity
{
    public override Guid Id { get; }
    public Guid UserId { get; }
    public Guid PageId { get; }
    public DateTime CreateDate { get; private set; }

    private GuestPage(Guid id, Guid userId, Guid pageId, DateTime createDate)
    {
        Id = id;
        UserId = userId;
        CreateDate = createDate;
        PageId = pageId;
    }

    public static GuestPage Create(Guid userId, Guid pageId)
    {
        var id = Guid.NewGuid();

        if (userId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid userId. Guid Empty.");

        if (pageId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid idPage. Guid Empty.");

        return new GuestPage(Guid.NewGuid(), userId, pageId, DateTime.Now);
    }
}