using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class RolePageRepository : RepositoryBase, IRolePageRepository
{
    public RolePageRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public async Task<RolePageModel> Add(RolePage entity)
    {
        var rolePageModel = _mapper.Map<RolePageDbModel>(entity);

        await _context.RolesPages.AddAsync(rolePageModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<RolePageModel>(rolePageModel);
    }

    public async Task<IEnumerable<RolePageModel>> GetAll()
    {
        return (await _context.RolesPages.ToListAsync())
            .Select(rolePageDb => _mapper.Map<RolePageModel>(rolePageDb));
    }

    public async Task<RolePageModel?> GetByIdOrDefault(Guid id)
    {
        var rolePageDb = await _context.RolesPages.FirstOrDefaultAsync(u => u.Id == id);

        if (rolePageDb is null)
            return null;

        return _mapper.Map<RolePageModel>(rolePageDb);
    }

    public async IAsyncEnumerable<RoleUserPageModel> GetByPage(Guid pageId)
    {
        IAsyncEnumerable<RoleUserPageModel> roleUserPages
            = (from rolePage in _context.RolesPages.AsAsyncEnumerable()
                join page in _context.Pages.AsAsyncEnumerable()
                on rolePage.PageId equals page.Id
                join user in _context.Users.AsAsyncEnumerable()
                on rolePage.UserId equals user.Id
                where rolePage.PageId == pageId
                select new RoleUserPageModel{
                    CreateDate = page.CreateDate,
                    Email = user.Email,
                    Id = rolePage.Id,
                    LastName = user.LastName,
                    Name = user.Name,
                    PageId = page.Id,
                    Role = rolePage.Role,
                    UserId = user.Id
                });

        await foreach (var roleUserPage in roleUserPages)
            yield return roleUserPage;
    }

    public async Task<RoleUserPageModel?> GetByPageAndUserOrDefault(Guid userId, Guid pageId)
    {
        IAsyncEnumerable<RoleUserPageModel> roleUserPages
            = (from rolePage in _context.RolesPages.AsAsyncEnumerable()
                join page in _context.Pages.AsAsyncEnumerable()
                on rolePage.PageId equals page.Id
                join user in _context.Users.AsAsyncEnumerable()
                on rolePage.UserId equals user.Id
                where rolePage.PageId == pageId
                where rolePage.UserId == userId
                select new RoleUserPageModel{
                    CreateDate = page.CreateDate,
                    Email = user.Email,
                    Id = rolePage.Id,
                    LastName = user.LastName,
                    Name = user.Name,
                    PageId = page.Id,
                    Role = rolePage.Role,
                    UserId = user.Id
                });

        return await roleUserPages.FirstOrDefaultAsync();
    }

    public async IAsyncEnumerable<RolePageModel> GetByUserOrDefault(Guid userId)
    {
        IAsyncEnumerable<RolePageModel> rolePages
            = (from rolePage in _context.RolesPages.AsAsyncEnumerable()
                join page in _context.Pages.AsAsyncEnumerable()
                on rolePage.PageId equals page.Id
                join user in _context.Users.AsAsyncEnumerable()
                on rolePage.UserId equals user.Id
                where user.Id == userId
                select new RolePageModel{
                    CreateDate = page.CreateDate,
                    Id = rolePage.Id,
                    PageId = page.Id,
                    Role = rolePage.Role,
                    UserId = user.Id
                });

        await foreach (var rolePage in rolePages)
            yield return rolePage;
    }

    public async Task<RoleUserPageModel?> GetRoleUserByIdOrDefault(Guid rolePageId)
    {
        IAsyncEnumerable<RoleUserPageModel> roleUserPages
            = (from rolePage in _context.RolesPages.AsAsyncEnumerable()
                join page in _context.Pages.AsAsyncEnumerable()
                on rolePage.PageId equals page.Id
                join user in _context.Users.AsAsyncEnumerable()
                on rolePage.UserId equals user.Id
                where rolePage.Id == rolePageId
                select new RoleUserPageModel{
                    CreateDate = page.CreateDate,
                    Email = user.Email,
                    Id = rolePage.Id,
                    LastName = user.LastName,
                    Name = user.Name,
                    PageId = page.Id,
                    Role = rolePage.Role,
                    UserId = user.Id
                });

        return await roleUserPages.FirstOrDefaultAsync();
    }

    public async Task<RolePageModel?> RemoveByIdOrDefault(Guid id)
    {
        var rolePageDb = await _context.RolesPages.FirstOrDefaultAsync(u => u.Id == id);

        if (rolePageDb is null)
            return null;

        _context.RolesPages.Remove(rolePageDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<RolePageModel>(rolePageDb);
    }

    public async Task<RolePageModel?> UpdateByIdOrDefault(Guid id, RolePage entity)
    {
        var rolePageDb = await _context.RolesPages.FirstOrDefaultAsync(u => u.Id == id);

        if (rolePageDb is null)
            return null;

        var rolePageToUpdate = _mapper.Map<RolePageDbModel>(entity);
        rolePageDb.CreateDate = rolePageToUpdate.CreateDate;
        rolePageDb.PageId = rolePageToUpdate.PageId;
        rolePageDb.Role = rolePageToUpdate.Role;
        rolePageDb.UserId = rolePageToUpdate.UserId;

        _context.RolesPages.Update(rolePageDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<RolePageModel>(
            await _context.RolesPages.FirstOrDefaultAsync(u => u.Id == id)
        );
    }
}