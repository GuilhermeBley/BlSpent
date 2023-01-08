namespace BlSpent.Core.Exceptions;

/// <summary>
/// Represets a exception with status code <see cref="System.Net.HttpStatusCode.BadRequest"/>
/// </summary>
public sealed class GenericCoreException : CoreException
{
    public const string DefaultMessage = "BadRequest";
    public override int StatusCode => (int)System.Net.HttpStatusCode.BadRequest;
    public GenericCoreException(string? message = DefaultMessage) : base(message)
    {
    }
}