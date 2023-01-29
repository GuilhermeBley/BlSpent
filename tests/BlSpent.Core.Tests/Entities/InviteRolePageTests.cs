namespace BlSpent.Core.Tests.Entities;

public class InviteRolePageTests
{
    [Fact]  
    public void Create_TryCreateInviteRolePage_Success()
    {
        Assert.NotNull(
            InviteRolePage.Create("valid@email.com", Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.ReadOnly.Value));
    }

    [Fact]  
    public void Create_TryCreateInviteRolePageWithEmptyOwnerGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            InviteRolePage.Create("valid@email.com", Guid.NewGuid(), Guid.Empty, Security.PageClaim.ReadOnly.Value)
        );
    }

    [Fact]  
    public void Create_TryCreateInviteRolePageWithEmptyPageGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            InviteRolePage.Create("valid@email.com", Guid.Empty, Guid.NewGuid(), Security.PageClaim.ReadOnly.Value)
        );
    }

    [Fact]  
    public void Create_TryCreateInviteRolePageWithInvalidEmail_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            InviteRolePage.Create("InvalidEmail", Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.ReadOnly.Value)
        );
    }

    [Fact]  
    public void Create_TryCreateInviteRolePageWithRole_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            InviteRolePage.Create("valid@email.com", Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.Owner.Value)
        );
    }
}