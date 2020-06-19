using System;

namespace SportManager.Common.Providers
{
    public interface IDateTimeProvider
    {
        
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}