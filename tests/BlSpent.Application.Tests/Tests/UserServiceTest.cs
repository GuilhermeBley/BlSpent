using Microsoft.Extensions.DependencyInjection;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Model;

namespace BlSpent.Application.Tests.Tests;

public class UserServiceTest : BaseTest
{
    [Fact]
    public async Task Create_TryCreateUser_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        Assert.NotNull(
            await userService.Create(
                Mocks.UserMock.ValidUser()
            )
        );
    }

    [Fact]
    public async Task Create_TryCreateInvalidPasswordUser_Failed()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userWithInvalidPassword = Mocks.UserMock.ValidUser();
        userWithInvalidPassword.Password = string.Empty;

        await Assert.ThrowsAnyAsync<Core.Exceptions.GenericCoreException>(
            () => userService.Create(
                userWithInvalidPassword
            )
        );
    }

    [Fact]
    public async Task Create_TryCreateInvalidEmailUser_Failed()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userWithInvalidEmail = Mocks.UserMock.ValidUser();
        userWithInvalidEmail.Email = "Invalid-Email";

        await Assert.ThrowsAnyAsync<Core.Exceptions.GenericCoreException>(
            () => userService.Create(
                userWithInvalidEmail
            )
        );
    }

    [Fact]
    public async Task GetByEmailAndPassword_TryLogin_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        await userService.Create(userToCreate);

        Assert.NotNull(
            await userService.GetByEmailAndPassword(userToCreate.Email, userToCreate.Password)
        );
    }

    [Fact]
    public async Task GetByEmailAndPassword_TryLoginWithInvalidEmail_Failed()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        await userService.Create(userToCreate);

        await Assert.ThrowsAnyAsync<Core.Exceptions.UnauthorizedCoreException>(
            () => userService.GetByEmailAndPassword("Invalid-Email", userToCreate.Password)
        );
    }

    [Fact]
    public async Task GetByEmailAndPassword_TryLoginWithInvalidPassword_Failed()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        await userService.Create(userToCreate);
        
        await Assert.ThrowsAnyAsync<Core.Exceptions.UnauthorizedCoreException>(
            () => userService.GetByEmailAndPassword(userToCreate.Email, "Invalid-Password")
        );
    }

    [Fact]
    public async Task GetByIdOrDefault_TryGetByIdOrDefault_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        var userCreated = await userService.Create(userToCreate);

        using var context = CreateContext(userCreated);

        Assert.NotNull(
            await userService.GetByIdOrDefault(userCreated.Id)
        );
    }
    
    [Fact]
    public async Task Update_TryUpdateUser_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        var userCreated = await userService.Create(userToCreate);

        var userToUpdate = userCreated;
        var oldName = userToUpdate.Name;
        userToUpdate.Name = "Updated"+userToUpdate.Name;

        using var context = CreateContext(userCreated);

        await userService.Update(userCreated.Id, userToUpdate);

        var userUpdated = await userService.GetByIdOrDefault(userCreated.Id);

        Assert.NotEqual(userUpdated?.Name, oldName);
    }
    
    [Fact]
    public async Task Update_TryUpdateUserAndCheckLogin_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        var userCreated = await userService.Create(userToCreate);

        var userToUpdate = userCreated;
        var oldName = userToUpdate.Name;
        userToUpdate.Name = "Updated"+userToUpdate.Name;

        using var context = CreateContext(userCreated);

        await userService.Update(userCreated.Id, userToUpdate);

        var userUpdated = await userService.GetByEmailAndPassword(userToCreate.Email, userToCreate.Password);

        Assert.NotNull(userUpdated);
    }
    
    [Fact]
    public async Task UpdatePassword_TryUpdateUserAndCheckLogin_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        var userCreated = await userService.Create(userToCreate);

        using var context = CreateContext(userCreated);

        var newDifferentPassword = Mocks.UserMock.NewValidPassword() + "123";
        await userService.UpdatePassword(userCreated.Id, userToCreate.Password, newDifferentPassword);

        var userUpdated = await userService.GetByEmailAndPassword(userToCreate.Email, newDifferentPassword);

        Assert.NotNull(userUpdated);
    }
    
    [Fact]
    public async Task UpdatePasswordForgot_TryUpdateUserAndCheckLogin_Success()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userToCreate = Mocks.UserMock.ValidUser();
        var userCreated = await userService.Create(userToCreate);

        using var context = CreateContext(userCreated, isNotRememberPassword: true);

        var newDifferentPassword = Mocks.UserMock.NewValidPassword() + "123";
        await userService.UpdatePasswordForgot(userCreated.Id, newDifferentPassword);

        var userUpdated = await userService.GetByEmailAndPassword(userToCreate.Email, newDifferentPassword);

        Assert.NotNull(userUpdated);
    }
}