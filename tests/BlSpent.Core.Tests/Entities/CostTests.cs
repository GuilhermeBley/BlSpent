namespace BlSpent.Core.Tests.Entities;

public class CostTests
{
    [Fact]  
    public void Create_TryCreateCost_Success()
    {
        var cost = Cost.Create(
            costDate: DateTime.Now, 
            value: -100.00, 
            pageId: Guid.NewGuid());

        Assert.NotNull(cost);
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateCost_Success()
    {
        var cost = Cost.CreateWithCurrentDate(
            value: -100.00, 
            pageId: Guid.NewGuid());

        Assert.NotNull(cost);
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateCostWithEmptyGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Cost.CreateWithCurrentDate(
            value: -100.00, 
            pageId: Guid.Empty)
        );
    }

    [Fact]  
    public void CreateWithCurrentDate_TryCreateCostWithPostiveValue_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Cost.CreateWithCurrentDate(
            value: 100.00, 
            pageId: Guid.NewGuid())
        );
    }

     [Fact]  
    public void Equals_TryCheckEquals_Failed()
    {
        var cost1 = Cost.Create(
            costDate: DateTime.Now, 
            value: -100.00, 
            pageId: Guid.NewGuid());

        var cost2 = Cost.Create(
            costDate: DateTime.Now, 
            value: -200.00, 
            pageId: Guid.NewGuid());

        Assert.False(cost1.Equals(cost2));
    }

    [Fact]  
    public void Equals_TryCheckEquals_Success()
    {
        var cost1 = Cost.Create(
            costDate: DateTime.Now, 
            value: -100.00, 
            pageId: Guid.NewGuid());

        Assert.True(cost1.Equals(cost1));
    }
}