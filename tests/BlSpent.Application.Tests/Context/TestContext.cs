using BlSpent.Application.Model;
using BlSpent.Application.Security;

namespace BlSpent.Application.Tests.Context;

internal class TestContext : ISecurityContext
{
    
    private static readonly AsyncLocal<ContextHolder> _contextCurrent = new AsyncLocal<ContextHolder>();

    /// <inheritdoc cref="IScrapContextAccessor.ScrapContext" path="*"/>
    public ClaimModel? ClaimContext
    {
        get { return _contextCurrent.Value?.claimContext; }
        set
        {
            var holder = _contextCurrent.Value;
            if (holder != null)
            {
                holder.claimContext = null;
            }

            if (value != null)
            {
                _contextCurrent.Value = new ContextHolder() { claimContext = value };
            }
        }
    }
    
    internal TestContext() { }

    /// <summary>
    /// Private unique holder class to context
    /// </summary>
    private class ContextHolder
    {
        public ClaimModel? claimContext;
    }

    public async Task<ClaimModel?> GetCurrentClaim()
    {
        await Task.CompletedTask;
        return ClaimContext;
    }
}