using BlSpent.Application.Model;

namespace BlSpent.Application.Tests.Mocks;

internal class PageMock
{
    public static PageModel ValidPage()
        => new PageModel
        {
            PageName = ValidNewNamePage()
        };
    public static string ValidNewNamePage()
        => Guid.NewGuid().ToString().Replace("-", string.Empty);
}