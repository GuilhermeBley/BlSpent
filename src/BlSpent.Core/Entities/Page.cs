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
    public const int MIN_LENGTH_PAGE_NAME = 2;
    public const int MAX_LENGTH_PAGE_NAME = 255;
    public const string ALLOWED_CHAR_PAGE_NAME = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";

    /// <summary>
    /// Identifier
    /// </summary>
    public override Guid Id { get; }

    /// <summary>
    /// Owner user
    /// </summary>
    public string PageName { get; }

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
    /// <param name="pageName"><inheritdoc cref="PageName" path="/summary"/></param>
    /// <param name="createDate"><inheritdoc cref="CreateDate" path="/summary"/></param>
    /// <param name="concurrencyStamp"><inheritdoc cref="ConcurrencyStamp" path="/summary"/></param>
    private Page(Guid id, string pageName, DateTime createDate, Guid concurrencyStamp)
    {
        Id = id;
        PageName = pageName;
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

        if (!this.PageName.Equals(page.PageName) ||
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
    /// <param name="pageName"><inheritdoc cref="PageName" path="/summary"/></param>
    /// <returns>new <see cref="Page"/></returns>
    /// <exception cref="GenericCoreException"></exception>
    public static Page Create(string pageName)
    {
        var id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(pageName))
            throw new GenericCoreException("Invalid page name. It is null or white space.");
        
        if (pageName.Length is < MIN_LENGTH_PAGE_NAME or > MAX_LENGTH_PAGE_NAME)
            throw new GenericCoreException($"Invalid legth page name. It must have between {MIN_LENGTH_PAGE_NAME} and {MAX_LENGTH_PAGE_NAME} of size.");

        if (pageName.Any(c => !ALLOWED_CHAR_PAGE_NAME.Contains(c)))
            throw new GenericCoreException($"Invalid character found in page name. Is allowed only '{ALLOWED_CHAR_PAGE_NAME}'.");

        return new Page(Guid.NewGuid(), pageName.Trim(), DateTime.Now, Guid.NewGuid());
    }
}