using BlSpent.Application.Model;
using BlSpent.Core.Entities;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;

namespace BlSpent.Application.Services.Implementation;

public class CostService : ICostService
{
    private readonly IUnitOfWork _uow;
    private readonly ICostRepository _costRepository;
    private readonly IPageRepository _pageRepository;

    public CostService(IUnitOfWork uow, 
        ICostRepository costRepository, IPageRepository pageRepository)
    {
        _uow = uow;
        _costRepository = costRepository;
        _pageRepository = pageRepository;
    }

    public async Task<CostModel> Add(CostModel model)
    {
        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(model.PageId)) is null)
            throw new PageNotFoundCoreException(model.PageId);

        return await _costRepository.Add(model);
    }

    public async Task<CostModel?> GetByIdOrDefault(Guid id)
    {
        using var transaction = await _uow.OpenConnectionAsync();
        return await _costRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<CostModel> GetByPageId(Guid pageId)
    {
        if (Guid.Empty == pageId)
            yield break;

        using (await _uow.OpenConnectionAsync())
            await foreach (var cost in _costRepository.GetByPageId(pageId))
            {
                yield return cost;
            }
    }

    public async Task<CostModel?> RemoveByIdOrDefault(Guid id)
    {
        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _costRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _costRepository.RemoveByIdOrDefault(id);
    }

    public async Task<CostModel?> UpdateByIdOrDefault(Guid id, CostModel model)
    {
         if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _costRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _costRepository.UpdateByIdOrDefault(id, model);
    }
}