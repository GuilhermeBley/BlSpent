namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a guest page
/// </summary>
public class GuestPage : Entity
{
    /// <summary>
    /// Id
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid PageId { get; }

    /// <summary>
    /// Create date of guest
    /// </summary>
    public DateTime CreateDate { get; private set; }

    /// <summary>
    /// Private instance
    /// </summary>
    /// <param name="id"><inheritdoc cref="Id" path="/summary"/></param>
    /// <param name="userId"><inheritdoc cref="UserId" path="/summary"/></param>
    /// <param name="pageId"><inheritdoc cref="PageId" path="/summary"/></param>
    /// <param name="createDate"><inheritdoc cref="CreateDate" path="/summary"/></param>
    private GuestPage(Guid id, Guid userId, Guid pageId, DateTime createDate)
    {
        Id = id;
        UserId = userId;
        CreateDate = createDate;
        PageId = pageId;
    }

    
    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var guestPage = obj as GuestPage;
        if (guestPage is null)
            return false;

        if (!this.UserId.Equals(guestPage.UserId) ||
            !this.PageId.Equals(guestPage.PageId) ||
            !this.CreateDate.Equals(guestPage.CreateDate))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 954734734;
    }

    /// <summary>
    /// Creates a new <see cref="GuestPage"/>
    /// </summary>
    /// <param name="userId"><inheritdoc cref="UserId" path="/summary"/></param>
    /// <param name="pageId"><inheritdoc cref="PageId" path="/summary"/></param>
    /// <returns>new <see cref="GuestPage"/></returns>
    /// <exception cref="GenericCoreException"></exception>
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