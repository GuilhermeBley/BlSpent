namespace BlSpent.Core.Entities;

public class Page : Entity
{
    public override Guid Id { get; }
    public Guid OwnerUserId { get; }
    public DateTime CreateDate { get; }
    public Guid ConcurrencyStamp { get; private set; }

    private Page(Guid id, Guid ownerUserId, DateTime createDate, Guid concurrencyStamp)
    {
        Id = id;
        OwnerUserId = ownerUserId;
        CreateDate = createDate;
        ConcurrencyStamp = concurrencyStamp;
    }

    public static Page Create(Guid ownerUserId)
    {
        var id = Guid.NewGuid();

        if (ownerUserId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid OwnerUser. Guid Empty.");

        return new Page(Guid.NewGuid(), ownerUserId, DateTime.Now, Guid.NewGuid());
    }
}