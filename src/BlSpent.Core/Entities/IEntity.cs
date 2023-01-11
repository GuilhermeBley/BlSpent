namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a entity
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Unique id
    /// </summary>
    Guid Id { get; }
}

/// <summary>
/// Pattern entity
/// </summary>
public abstract class Entity : IEntity
{
    public virtual Guid Id { get; } = Guid.NewGuid();
    
    /// <summary>
    /// Check entities
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        var entity = obj as IEntity;
        if (entity is null)
            return false;

        return (base.Equals(obj) && this.Id.Equals(entity.Id));
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 162731263;
    }

    public override string ToString()
    {
        return $"{Id.ToString()}|{base.ToString()}";
    }
}