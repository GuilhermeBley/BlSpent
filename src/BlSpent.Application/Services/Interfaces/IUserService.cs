using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Creates new user
    /// </summary>
    /// <param name="userModel">User to create</param>
    /// <returns>User created</returns>
    /// <exception cref="Core.Exceptions.ConflictCoreException"></exception>
    Task<UserModel> Create(UserModel userModel);
    
    /// <summary>
    /// Get current user by email, if current user is different of <paramref name="email"/>, throws exception
    /// </summary>
    /// <exception cref="Core.Exceptions.UnauthorizedCoreException"></exception>
    /// <exception cref="Core.Exceptions.ForbiddenCoreException"></exception>
    Task<UserModel?> GetByEmailOrDefault(string email);

    /// <summary>
    /// Get current user by id, if current user is different of <paramref name="id"/>, throws exception
    /// </summary>
    /// <exception cref="Core.Exceptions.UnauthorizedCoreException"></exception>
    /// <exception cref="Core.Exceptions.ForbiddenCoreException"></exception>
    Task<UserModel?> GetByIdOrDefault(Guid id);

    /// <summary>
    /// Update by ID
    /// </summary>
    /// <param name="id">Id to update</param>
    /// <param name="userModel">Values which update</param>
    /// <returns>Model updated or null if not found.</returns>
    /// <exception cref="Core.Exceptions.UnauthorizedCoreException"></exception>
    /// <exception cref="Core.Exceptions.ForbiddenCoreException"></exception>
    Task<UserModel?> Update(Guid id, UserModel userModel);

    /// <summary>
    /// If valid login and password, return userModel
    /// </summary>
    /// <returns>User model found.</returns>
    /// <exception cref="Core.Exceptions.UnauthorizedCoreException"></exception>
    Task<UserModel> GetByEmailAndPassword(string email, string password);
}