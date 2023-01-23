using BlSpent.Core.Exceptions;

namespace BlSpent.Application.Exceptions;

public class PageNotFoundCoreException : NotFoundCoreException
{
    public PageNotFoundCoreException(Guid idPage)
        : base($"Page with id '{idPage}' was not found.")
    {
    }
}