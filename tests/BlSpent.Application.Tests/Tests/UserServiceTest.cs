using Microsoft.Extensions.DependencyInjection;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Model;

namespace BlSpent.Application.Tests.Tests;

public class UserServiceTest : BaseTest
{
    private const string VALID_PASSWORD = "";
    private const string VALID_EMAIL = "";
    private const string VALID_NAME = "name";
    private const string VALID_LAST_NAME = "last name";

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

    private static UserModel ValidUser()
        => new UserModel
        {
            Password = "a1@12345678",
            Email = "teste@email.com",
            Name = "name",
            LastName = "last name"
        };
}