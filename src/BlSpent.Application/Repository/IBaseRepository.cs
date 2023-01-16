namespace BlSpent.Application.Repository;

/// <summary>
/// Base repository
/// </summary>
/// <typeparam name="TModel">Model</typeparam>
/// <typeparam name="TEntity">Core Entity</typeparam>
/// <typeparam name="TId">Id</typeparam>
public interface IBaseRepository<TModel, TEntity, TId>
    where TModel : class
    where TEntity : class
{
    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity">entity to add</param>
    /// <returns>model added</returns>
    Task<TModel> Add(TEntity entity);

    /// <summary>
    /// get all models
    /// </summary>
    /// <returns>enumerable of model</returns>
    Task<IEnumerable<TModel>> GetAll();

    /// <summary>
    /// Gets the model or default
    /// </summary>
    /// <param name="id">id to find</param>
    /// <returns>model, or null if not found</returns>
    Task<TModel?> GetByIdOrDefault(TId id);

    /// <summary>
    /// Remove by id or default
    /// </summary>
    /// <param name="id">id to delete</param>
    /// <returns>model removed, or null if don't find</returns>
    Task<TModel?> RemoveByIdOrDefault(TId id);

    /// <summary>
    /// Update by id or default
    /// </summary>
    /// <param name="id">id to update</param>
    /// <param name="entity">data to update</param>
    /// <returns>model updated, or null if don't find</returns>
    Task<TModel?> UpdateByIdOrDefault(TId id, TEntity entity);
}