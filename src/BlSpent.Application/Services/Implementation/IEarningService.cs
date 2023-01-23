using BlSpent.Application.Model;
using BlSpent.Core.Entities;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;

namespace BlSpent.Application.Services.Implementation;

public class EarningService : IEarningService
{
    private readonly IUnitOfWork _uow;
    private readonly IEarningRepository _earningRepository;
    private readonly IPageRepository _pageRepository;

    public EarningService(IUnitOfWork uow, 
        IEarningRepository earningRepository, IPageRepository pageRepository)
    {
        _uow = uow;
        _earningRepository = earningRepository;
        _pageRepository = pageRepository;
    }

    public async Task<EarningModel> Add(Earning entity)
    {
        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(entity.PageId)) is null)
            throw new PageNotFoundCoreException(entity.PageId);

        return await _earningRepository.Add(entity);
    }

    public async Task<EarningModel?> GetByIdOrDefault(Guid id)
    {
        using var transaction = await _uow.OpenConnectionAsync();
        return await _earningRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<EarningModel> GetByPageId(Guid pageId)
    {
        if (Guid.Empty == pageId)
            yield break;

        using (await _uow.OpenConnectionAsync())
            await foreach (var earning in _earningRepository.GetByPageId(pageId))
            {
                yield return earning;
            }
    }

    public async Task<EarningModel?> RemoveByIdOrDefault(Guid id)
    {
        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _earningRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _earningRepository.RemoveByIdOrDefault(id);
    }

    public async Task<EarningModel?> UpdateByIdOrDefault(Guid id, Earning entity)
    {
         if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _earningRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _earningRepository.UpdateByIdOrDefault(id, entity);
    }
}