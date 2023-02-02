using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserModel> Create(UserModel userModel);
    Task<UserModel?> GetByEmailOrDefault(string email);
    Task<UserModel?> GetByIdOrDefault(Guid id);
    Task<UserModel?> Update(Guid id, UserModel userModel);
    Task<UserModel?> UpdatePassword(Guid id, UserModel userModel);
}