using BlSpent.Application.Model;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;
using BlSpent.Application.Security;

namespace BlSpent.Application.Services.Implementation;

public class CostService : BaseService, ICostService
{
    private readonly IUnitOfWork _uow;
    private readonly ICostRepository _costRepository;
    private readonly IPageRepository _pageRepository;

    public CostService(IUnitOfWork uow, 
        ICostRepository costRepository, IPageRepository pageRepository,
        ISecurityContext securityContext)
        : base(securityContext)
    {
        _uow = uow;
        _costRepository = costRepository;
        _pageRepository = pageRepository;
    }

    public async Task<CostModel> Add(CostModel model)
    {
        await _securityChecker.ThrowIfCantModify();

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(model.PageId)) is null)
            throw new PageNotFoundCoreException(model.PageId);

        var costAdded = await _costRepository.Add(Mappings.Mapper.Map(model));

        await _uow.SaveChangesAsync();

        return costAdded;
    }

    public async Task<CostModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfCantRead();

        using var transaction = await _uow.OpenConnectionAsync();
        return await _costRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<CostModel> GetByPageId(Guid pageId)
    {
        await _securityChecker.ThrowIfCantRead();

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
        await _securityChecker.ThrowIfCantModify();

        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _costRepository.GetByIdOrDefault(id)) is null)
            return null;

        var costRemoved = await _costRepository.RemoveByIdOrDefault(id);

        await _uow.SaveChangesAsync();

        return costRemoved;
    }

    public async Task<CostModel?> UpdateByIdOrDefault(Guid id, CostModel model)
    {
        await _securityChecker.ThrowIfCantModify();

         if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _costRepository.GetByIdOrDefault(id)) is null)
            return null;

        var costUpdated = await _costRepository.UpdateByIdOrDefault(id, Mappings.Mapper.Map(model));

        await _uow.SaveChangesAsync();

        return costUpdated;
    }
}