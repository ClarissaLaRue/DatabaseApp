using System;

namespace SportManager.Common.Providers
{
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); }
        }

        public DateTime UtcNow
        {
            get { return DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc); }
        }
    }
}