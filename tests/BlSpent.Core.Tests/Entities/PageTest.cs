namespace BlSpent.Core.Tests.Entities;

public class PageTest
{
    [Fact]  
    public void Create_TryCreatePage_Success()
    {
        var page = Page.Create(Guid.NewGuid());

        Assert.NotNull(page);
    }

    [Fact]  
    public void Create_TryCreateWithEmptyGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(
            () => Page.Create(Guid.Empty)
        );
    }
}