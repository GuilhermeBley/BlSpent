namespace BlSpent.Core.Tests.Entities;

public class PageTest
{
    [Fact]  
    public void Create_TryCreatePage_Success()
    {
        var page = Page.Create("Valid Name 123");

        Assert.NotNull(page);
    }

    [Fact]  
    public void Create_TryCreateInvalidName_Failed()
    {
        Assert.ThrowsAny<CoreException>(
            () => Page.Create("Invalid Name.")
        );
    }

    [Fact]  
    public void Create_TryCreateWithEmptyGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(
            () => Page.Create(@"Invalid Name\n")
        );
    }
}