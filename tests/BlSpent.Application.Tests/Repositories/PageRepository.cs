using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class PageRepository : RepositoryBase, IPageRepository
{
    public PageRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public async Task<PageModel> Add(Page entity)
    {
        var pageModel = _mapper.Map<PageDbModel>(entity);

        await _context.Pages.AddAsync(pageModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<PageModel>(pageModel);
    }

    public async Task<IEnumerable<PageModel>> GetAll()
    {
        return (await _context.Pages.ToListAsync())
            .Select(pageDb => _mapper.Map<PageModel>(pageDb));
    }

    public async Task<PageModel?> GetByIdOrDefault(Guid id)
    {
        var pageDb = await _context.Pages.FirstOrDefaultAsync(u => u.Id == id);

        if (pageDb is null)
            return null;

        return _mapper.Map<PageModel>(pageDb);
    }

    public async IAsyncEnumerable<PageModel> GetPagesWhichUserCanAccess(Guid userId)
    {
        var pagesDbAccess
            = (from rolePage in _context.RolesPages.AsAsyncEnumerable()
               join page in _context.Pages.AsAsyncEnumerable()
               on rolePage.PageId equals page.Id
               where rolePage.UserId == userId
               select page);

        await foreach (var pageDb in pagesDbAccess)
            yield return _mapper.Map<PageModel>(pageDb);
    }

    public async Task<PageModel?> RemoveByIdOrDefault(Guid id)
    {
        var pageDb = await _context.Pages.FirstOrDefaultAsync(u => u.Id == id);

        if (pageDb is null)
            return null;

        _context.Pages.Remove(pageDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<PageModel>(pageDb);
    }

    public async Task<PageModel?> UpdateByIdOrDefault(Guid id, Page entity)
    {
        var pageDb = await _context.Pages.FirstOrDefaultAsync(u => u.Id == id);

        if (pageDb is null)
            return null;

        var pageToUpdate = _mapper.Map<PageDbModel>(entity);
        pageDb.ConcurrencyStamp = pageToUpdate.ConcurrencyStamp;
        pageDb.CreateDate = pageToUpdate.CreateDate;
        pageDb.PageName = pageToUpdate.PageName;

        _context.Pages.Update(pageDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<PageModel>(
            await _context.Pages.FirstOrDefaultAsync(u => u.Id == id)
        );
    }
}