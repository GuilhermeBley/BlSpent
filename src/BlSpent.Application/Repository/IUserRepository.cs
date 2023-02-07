using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IUserRepository : IBaseRepository<UserModel, User, Guid>
{
    Task<UserModel> GetByEmail(string email);
}