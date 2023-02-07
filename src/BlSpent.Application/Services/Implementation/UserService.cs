using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Security.Internal;
using BlSpent.Application.Security;

namespace BlSpent.Application.Services.Implementation;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uoW;

    public UserService(
        ISecurityContext securityContext,
        IUserRepository userRepository,
        IUnitOfWork uoW)
        : base (securityContext)
    {
        _userRepository = userRepository;
        _uoW = uoW;
    }
    
    public async Task<UserModel> Create(UserModel userModel)
    {
        var entityUser = CreateUserWithHashedPassword(userModel);

        using var transaction = await _uoW.BeginTransactionAsync();

        if ((await _userRepository.GetByEmail(entityUser.Email)) is not null)
            throw new Core.Exceptions.ConflictCoreException($"Email '{entityUser.Email}' already registered.");

        var userCreated = await _userRepository.Add(entityUser);

        await transaction.SaveChangesAsync();

        return userCreated;
    }

    public async Task<UserModel?> GetByEmailOrDefault(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new Core.Exceptions.GenericCoreException("Invalid email.");

        await _securityChecker.ThrowIfIsntLogged();

        if ((await _securityContext.GetCurrentClaim())?.Email != email)
            throw new Core.Exceptions.ForbiddenCoreException("Can't access this email.");

        using var connection = await _uoW.OpenConnectionAsync();

        var userModel = await _userRepository.GetByEmail(email);
        
        if (userModel is not null)
        {
            userModel.Password = string.Empty;
            userModel.PasswordHash = string.Empty;
            userModel.Salt = string.Empty;
        }

        return userModel;
    }

    public async Task<UserModel?> GetByIdOrDefault(Guid id)
    {
        if (id == default)
            throw new Core.Exceptions.GenericCoreException("Invalid id.");

        await _securityChecker.ThrowIfIsntLogged();

        if ((await _securityContext.GetCurrentClaim())?.UserId != id)
            throw new Core.Exceptions.ForbiddenCoreException("Can't access this Id.");

        using var connection = await _uoW.OpenConnectionAsync();

        var userModel = await _userRepository.GetByIdOrDefault(id);

        if (userModel is not null)
        {
            userModel.Password = string.Empty;
            userModel.PasswordHash = string.Empty;
            userModel.Salt = string.Empty;
        }

        return userModel;
    }

    public async Task<UserModel?> Update(Guid id, UserModel userModel)
    {
        if (id == default)
            throw new Core.Exceptions.GenericCoreException("Invalid id.");

        await _securityChecker.ThrowIfIsntLogged();

        var currentUser = await _securityContext.GetCurrentClaim() 
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        if (id != currentUser.UserId)
            throw new Core.Exceptions.ForbiddenCoreException("Current user can't access this Id.");

        var entityUser = CreateUserWithHashedPassword(userModel);

        using var transaction = await _uoW.BeginTransactionAsync();

        if ((await _userRepository.GetByIdOrDefault(id)) is null)
            return null;

        var userUpdated = await _userRepository.UpdateByIdOrDefault(id, entityUser);

        await transaction.SaveChangesAsync();

        return userUpdated;
    }

    public async Task<UserModel> GetByEmailAndPassword(string email, string password)
    {
        if (string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password))
            throw new Core.Exceptions.UnauthorizedCoreException();

        using var connection = await _uoW.OpenConnectionAsync();

        var userToCheckPassword = await _userRepository.GetByEmail(email)
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        if (!PasswordCrypt.IsValidPassword(password, userToCheckPassword.PasswordHash, userToCheckPassword.Salt))
            throw new Core.Exceptions.UnauthorizedCoreException();

        userToCheckPassword.Password = string.Empty;
        userToCheckPassword.PasswordHash = string.Empty;
        userToCheckPassword.Salt = string.Empty;

        return userToCheckPassword;
    }

    private Core.Entities.User CreateUserWithHashedPassword(UserModel? userModel)
    {
        if (userModel is null)
            throw new Core.Exceptions.GenericCoreException("Empty user.");

        Core.Entities.User.ThrowIfIsInvalidPassword(userModel.Password);

        var hashedResult = PasswordCrypt.HashPasword(userModel.Password);

        userModel.PasswordHash = hashedResult.Hash;
        userModel.Salt = hashedResult.Salt;

        return Mappings.Mapper.Map(userModel);
    }
}