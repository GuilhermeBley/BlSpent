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
                ValidUser()
            )
        );
    }

    [Fact]
    public async Task Create_TryCreateInvalidPasswordUser_Failed()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        var userWithInvalidPassword = ValidUser();
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

        var userWithInvalidEmail = ValidUser();
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

        var userToCreate = ValidUser();
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

        var userToCreate = ValidUser();
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

        var userToCreate = ValidUser();
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

        var userToCreate = ValidUser();
        var userCreated = await userService.Create(userToCreate);

        SetContext(userCreated);

        Assert.NotNull(
            await userService.GetByIdOrDefault(userCreated.Id)
        );
    }

    private static UserModel ValidUser()
        => new UserModel
        {
            Password = "a1@12345678",
            Email = $"{Guid.NewGuid()}@email.com",
            Name = "name",
            LastName = "last name"
        };
}