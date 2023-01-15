namespace BlSpent.Core.Tests.Entities;

public class GuestPageTest
{
    [Fact]  
    public void Create_TryCreateGuestPage_Success()
    {
        var guestPage = GuestPage.Create(Guid.NewGuid(), Guid.NewGuid());

        Assert.NotNull(guestPage);
    }

    [Fact]  
    public void Create_TryCreateWithEmptyUserId_Success()
    {
        Assert.ThrowsAny<CoreException>(() =>
            GuestPage.Create(Guid.Empty, Guid.NewGuid()));
    }

    [Fact]  
    public void Create_TryCreateWithEmptyPageId_Success()
    {
        Assert.ThrowsAny<CoreException>(() =>
            GuestPage.Create(Guid.NewGuid(), Guid.Empty));
    }
}