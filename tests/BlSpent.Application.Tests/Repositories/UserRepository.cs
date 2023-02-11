using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Core.Entities;

namespace BlSpent.Application.Tests.Repositories;

internal class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public Task<UserModel> Add(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> RemoveByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> UpdateByIdOrDefault(Guid id, User entity)
    {
        throw new NotImplementedException();
    }
}