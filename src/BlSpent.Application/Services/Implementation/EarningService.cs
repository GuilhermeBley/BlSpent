using BlSpent.Application.Model;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;
using BlSpent.Application.Security;

namespace BlSpent.Application.Services.Implementation;

public class EarningService : BaseService, IEarningService
{
    private readonly IUnitOfWork _uow;
    private readonly IEarningRepository _earningRepository;
    private readonly IPageRepository _pageRepository;

    public EarningService(IUnitOfWork uow, 
        IEarningRepository earningRepository, 
        IPageRepository pageRepository,
        ISecurityContext securityContext)
        : base(securityContext)
    {
        _uow = uow;
        _earningRepository = earningRepository;
        _pageRepository = pageRepository;
    }

    public async Task<EarningModel> Add(EarningModel model)
    {
        await _securityChecker.ThrowIfCantModify();

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(model.PageId)) is null)
            throw new PageNotFoundCoreException(model.PageId);

        var earnAdded = await _earningRepository.Add(Mappings.Mapper.Map(model));

        await _uow.SaveChangesAsync();

        return earnAdded;
    }

    public async Task<EarningModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfCantRead();

        using var transaction = await _uow.OpenConnectionAsync();
        return await _earningRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<EarningModel> GetByPageId(Guid pageId)
    {
        await _securityChecker.ThrowIfCantRead();

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
        await _securityChecker.ThrowIfCantModify();
        
        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _earningRepository.GetByIdOrDefault(id)) is null)
            return null;

        var earnRemoved = await _earningRepository.RemoveByIdOrDefault(id);

        await _uow.SaveChangesAsync();

        return earnRemoved;
    }

    public async Task<EarningModel?> UpdateByIdOrDefault(Guid id, EarningModel model)
    {
        await _securityChecker.ThrowIfCantModify();

        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _earningRepository.GetByIdOrDefault(id)) is null)
            return null;

        var earnUpdated = await _earningRepository.UpdateByIdOrDefault(id, Mappings.Mapper.Map(model));

        await _uow.SaveChangesAsync();

        return earnUpdated;
    }
}