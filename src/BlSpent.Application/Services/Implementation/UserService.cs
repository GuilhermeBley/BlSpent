using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Services.Interfaces;

namespace BlSpent.Application.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Task<UserModel> Create(UserModel userModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetByEmailOrDefault(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> Update(Guid id, UserModel userModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> UpdatePassword(Guid id, UserModel userModel)
    {
        throw new NotImplementedException();
    }
}