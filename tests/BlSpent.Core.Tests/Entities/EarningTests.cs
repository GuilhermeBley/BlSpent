namespace BlSpent.Core.Tests.Entities;

public class EarningTests
{
    [Fact]  
    public void Create_TryCreateEarning_Success()
    {
        var earning = Earning.Create(
            earnDate: DateTime.Now, 
            value: 100.00, 
            pageId: Guid.NewGuid());

        Assert.NotNull(earning);
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateEarning_Success()
    {
        var earning = Earning.CreateWithCurrentDate(
            value: 100.00, 
            pageId: Guid.NewGuid());

        Assert.NotNull(earning);
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateEarningWithEmptyGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Earning.CreateWithCurrentDate(
            value: 100.00, 
            pageId: Guid.Empty)
        );
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateEarningWithNegativeValue_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Earning.CreateWithCurrentDate(
            value: -100.00, 
            pageId: Guid.NewGuid())
        );
    }

     [Fact]  
    public void Equals_TryCheckEquals_Failed()
    {
        var earning1 = Earning.Create(
            earnDate: DateTime.Now, 
            value: 100.00, 
            pageId: Guid.NewGuid());

        var earning2 = Earning.Create(
            earnDate: DateTime.Now, 
            value: 200.00, 
            pageId: Guid.NewGuid());

        Assert.False(earning1.Equals(earning2));
    }

    [Fact]  
    public void Equals_TryCheckEquals_Success()
    {
        var earning1 = Earning.Create(
            earnDate: DateTime.Now, 
            value: 100.00, 
            pageId: Guid.NewGuid());

        Assert.True(earning1.Equals(earning1));
    }
}