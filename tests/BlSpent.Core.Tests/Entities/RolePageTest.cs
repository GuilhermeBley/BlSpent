namespace BlSpent.Core.Tests.Entities;

public class RolePageTest
{
    [Fact]  
    public void Create_TryCreateRolePageReadOnly_Success()
    {
        var rolePage = 
            RolePage.Create(Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.ReadOnly.Value);

        Assert.NotNull(rolePage);
    }

    [Fact]  
    public void Create_TryCreateRolePageModifier_Success()
    {
        var rolePage = 
            RolePage.Create(Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.Modifier.Value);

        Assert.NotNull(rolePage);
    }
    
    [Fact]  
    public void Create_TryCreateRolePageOwner_Success()
    {
        var rolePage = 
            RolePage.Create(Guid.NewGuid(), Guid.NewGuid(), Security.PageClaim.Owner.Value);

        Assert.NotNull(rolePage);
    }
    
    [Fact]  
    public void CreateReadOnlyRolePage_TryCreateRole_Success()
    {
        var rolePage = 
            RolePage.CreateReadOnlyRolePage(Guid.NewGuid(), Guid.NewGuid());

        Assert.NotNull(rolePage);
    }
    
    [Fact]  
    public void CreateModifierRolePage_TryCreateRole_Success()
    {
        var rolePage = 
            RolePage.CreateModifierRolePage(Guid.NewGuid(), Guid.NewGuid());

        Assert.NotNull(rolePage);
    }
    
    [Fact]  
    public void CreateOwnerRolePage_TryCreateRole_Success()
    {
        var rolePage = 
            RolePage.CreateOwnerRolePage(Guid.NewGuid(), Guid.NewGuid());

        Assert.NotNull(rolePage);
    }

    [Fact]  
    public void CreateOwnerRolePage_TryCreateWithEmptyUserId_Success()
    {
        Assert.ThrowsAny<CoreException>(() =>
            RolePage.CreateOwnerRolePage(Guid.Empty, Guid.NewGuid()));
    }

    [Fact]  
    public void CreateOwnerRolePage_TryCreateWithEmptyPageId_Success()
    {
        Assert.ThrowsAny<CoreException>(() =>
            RolePage.CreateOwnerRolePage(Guid.NewGuid(), Guid.Empty));
    }

    
    [Fact]  
    public void Create_TryCreateWithInvalidRole_Success()
    {
        Assert.ThrowsAny<CoreException>(() =>
            RolePage.Create(Guid.NewGuid(), Guid.NewGuid(), "INVALID ROLE"));
    }
}