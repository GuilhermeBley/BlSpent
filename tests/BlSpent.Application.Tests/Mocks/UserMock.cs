using BlSpent.Application.Model;

namespace BlSpent.Application.Tests.Mocks;

internal class UserMock
{
    public static UserModel ValidUser()
        => new UserModel
        {
            Password = NewValidPassword(),
            Email = $"{Guid.NewGuid()}@email.com",
            Name = "name",
            LastName = "last name"
        };

    public static string NewValidPassword()
        => "a1@12345678";

}