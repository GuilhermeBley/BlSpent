using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IUserRepository : IBaseRepository<UserModel, User, Guid>
{
    Task<UserModel?> GetByEmail(string email);

    /// <summary>
    /// Update by id or default
    /// </summary>
    /// <remarks>
    ///     <para>Update all fields, except password fields, as hash and salt</para>
    /// </remarks>
    /// <param name="id">id to update</param>
    /// <param name="entity">data to update</param>
    /// <returns>model updated, or null if don't find</returns>
    new Task<UserModel?> UpdateByIdOrDefault(Guid id, User entity);

    /// <summary>
    /// Update password fields by id or default
    /// </summary>
    /// <param name="id">id to update</param>
    /// <param name="entity">data with password to update</param>
    /// <returns>model updated, or null if don't find</returns>
    Task<UserModel?> UpdatePasswordByIdOrDefault(Guid id, User entity);
}