using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public async Task<UserModel> Add(User entity)
    {
        var userModel = _mapper.Map<UserDbModel>(entity);

        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserModel>(userModel);
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return (await _context.Users.ToListAsync())
            .Select(userDb => _mapper.Map<UserModel>(userDb));
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (userDb is null)
            return null;

        return _mapper.Map<UserModel>(userDb);
    }

    public async Task<UserModel?> GetByIdOrDefault(Guid id)
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userDb is null)
            return null;

        return _mapper.Map<UserModel>(userDb);
    }

    public async Task<UserModel?> RemoveByIdOrDefault(Guid id)
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userDb is null)
            return null;

        _context.Users.Remove(userDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserModel>(userDb);
    }

    public async Task<UserModel?> UpdateByIdOrDefault(Guid id, User entity)
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userDb is null)
            return null;

        var userToUpdate = _mapper.Map<UserDbModel>(entity);
        userDb.AccessFailedCount = userToUpdate.AccessFailedCount;
        userDb.Email = userToUpdate.Email;
        userDb.EmailConfirmed = userToUpdate.EmailConfirmed;
        userDb.LastName = userToUpdate.LastName;
        userDb.LockOutEnabled = userToUpdate.LockOutEnabled;
        userDb.Name = userToUpdate.Name;
        userDb.PasswordHash = userToUpdate.PasswordHash;
        userDb.PhoneNumber = userToUpdate.PhoneNumber;
        userDb.PhoneNumberConfirmed = userToUpdate.PhoneNumberConfirmed;
        userDb.Salt = userToUpdate.Salt;
        userDb.TwoFactoryEnabled = userToUpdate.TwoFactoryEnabled;
        userDb.UserName = userToUpdate.UserName;

        _context.Users.Update(userDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserModel>(
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
        );
    }

    public Task<UserModel> UpdatePasswordByIdOrDefault(Guid id, User entity)
    {
        throw new NotImplementedException();
    }
}