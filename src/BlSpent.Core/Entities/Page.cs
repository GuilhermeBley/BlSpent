namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a page
/// </summary>
/// <remarks>
///     <para>A page contains monetary values.</para>
///     <para>All pages must contains a owner user.</para>
/// </remarks>
public class Page : Entity
{
    /// <summary>
    /// Identifier
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// Owner user
    /// </summary>
    public Guid OwnerUserId { get; }

    /// <summary>
    /// Create date of page
    /// </summary>
    public DateTime CreateDate { get; }

    /// <summary>
    /// Concurrency stamp, represents last refresh
    /// </summary>
    public Guid ConcurrencyStamp { get; private set; }

    /// <summary>
    /// Private instance
    /// </summary>
    /// <param name="id"><inheritdoc cref="Id" path="/summary"/></param>
    /// <param name="ownerUserId"><inheritdoc cref="OwnerUserId" path="/summary"/></param>
    /// <param name="createDate"><inheritdoc cref="CreateDate" path="/summary"/></param>
    /// <param name="concurrencyStamp"><inheritdoc cref="ConcurrencyStamp" path="/summary"/></param>
    private Page(Guid id, Guid ownerUserId, DateTime createDate, Guid concurrencyStamp)
    {
        Id = id;
        OwnerUserId = ownerUserId;
        CreateDate = createDate;
        ConcurrencyStamp = concurrencyStamp;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var page = obj as Page;
        if (page is null)
            return false;

        if (!this.OwnerUserId.Equals(page.OwnerUserId) ||
            !this.CreateDate.Equals(page.CreateDate) ||
            !this.ConcurrencyStamp.Equals(page.ConcurrencyStamp))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 1278361278;
    }

    /// <summary>
    /// Creates new <see cref="Page"/>
    /// </summary>
    /// <param name="ownerUserId"><inheritdoc cref="OwnerUserId" path="/summary"/></param>
    /// <returns>new <see cref="Page"/></returns>
    /// <exception cref="GenericCoreException"></exception>
    public static Page Create(Guid ownerUserId)
    {
        var id = Guid.NewGuid();

        if (ownerUserId.Equals(Guid.Empty))
            throw new GenericCoreException("Invalid OwnerUser. Guid Empty.");

        return new Page(Guid.NewGuid(), ownerUserId, DateTime.Now, Guid.NewGuid());
    }
}