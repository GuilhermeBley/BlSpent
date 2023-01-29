namespace BlSpent.Core.Tests.Entities;

public class UserTests
{
    [Fact]  
    public void Create_TryCreateUser_Success()
    {
        var user = User.Create(
                email: "teste@email.com",
                emailConfirmed: false,
                phoneNumber: null,
                phoneNumberConfirmed: false,
                twoFactoryEnabled: true,
                lockOutEnd: null,
                lockOutEnabled: false,
                accessFailedCount: 0,
                name: "teste",
                lastName: "teste1 teste2",
                password: "teste@123",
                string.Empty,
                string.Empty);

        Assert.NotNull(user);
    }

    [Fact]  
    public void Create_TryCreateUserWithEmptyEmail_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            User.Create(
                email: string.Empty,
                emailConfirmed: false, 
                phoneNumber: "15abc", 
                phoneNumberConfirmed: false, 
                twoFactoryEnabled: true, 
                lockOutEnd: null, 
                lockOutEnabled: false, 
                accessFailedCount: 0, 
                name: "teste", 
                lastName: "teste1 teste2", 
                password: "teste",
                string.Empty,
                string.Empty)
        );
    }
    
    [Fact]  
    public void Create_TryCreateUserWithLettersInPhoneNumber_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            User.Create(
                email: "teste@email.com", 
                emailConfirmed: false, 
                phoneNumber: "15abc", 
                phoneNumberConfirmed: false, 
                twoFactoryEnabled: true, 
                lockOutEnd: null, 
                lockOutEnabled: false, 
                accessFailedCount: 0, 
                name: "teste", 
                lastName: "teste1 teste2", 
                password: "teste",
                string.Empty,
                string.Empty)
        );
    }
    
    [Fact]  
    public void Create_TryCreateUserWithInvalidPassword_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            User.Create(
                email: "teste@email.com", 
                emailConfirmed: false, 
                phoneNumber: null, 
                phoneNumberConfirmed: false, 
                twoFactoryEnabled: true, 
                lockOutEnd: null, 
                lockOutEnabled: false, 
                accessFailedCount: 0, 
                name: "teste", 
                lastName: "teste1 teste2", 
                password: "teste",
                string.Empty,
                string.Empty)
        );
    }

    [Fact]  
    public void Equals_TryCheckEquals_Failed()
    {
        var user1 = User.Create(
                email: "user1@email.com",
                emailConfirmed: false,
                phoneNumber: null,
                phoneNumberConfirmed: false,
                twoFactoryEnabled: true,
                lockOutEnd: null,
                lockOutEnabled: false,
                accessFailedCount: 0,
                name: "user1",
                lastName: "teste1 teste2",
                password: "user1@123",
                string.Empty,
                string.Empty);

        var user2 = User.Create(
            email: "user2@email.com",
            emailConfirmed: false,
            phoneNumber: null,
            phoneNumberConfirmed: false,
            twoFactoryEnabled: true,
            lockOutEnd: null,
            lockOutEnabled: false,
            accessFailedCount: 0,
            name: "user2",
            lastName: "teste2 teste2",
            password: "user2@123",
            string.Empty,
            string.Empty);

        Assert.False(user1.Equals(user2));
    }

    [Fact]  
    public void Equals_TryCheckEquals_Success()
    {
        var user1 = User.Create(
            email: "user1@email.com",
            emailConfirmed: false,
            phoneNumber: null,
            phoneNumberConfirmed: false,
            twoFactoryEnabled: true,
            lockOutEnd: null,
            lockOutEnabled: false,
            accessFailedCount: 0,
            name: "user1",
            lastName: "teste1 teste2",
            password: "user1@123",
            string.Empty,
            string.Empty);

        Assert.True(user1.Equals(user1));
    }

    [Fact]  
    public void Create_CheckEmail_Failed()
    {
        Assert.Throws<GenericCoreException>(() => User.Create(
            email: "InvalidEmial",
            emailConfirmed: false,
            phoneNumber: null,
            phoneNumberConfirmed: false,
            twoFactoryEnabled: true,
            lockOutEnd: null,
            lockOutEnabled: false,
            accessFailedCount: 0,
            name: "user1",
            lastName: "teste1 teste2",
            password: "user1@123",
            string.Empty,
            string.Empty));
    }
}