using Assessment.Health.Service.Contract;
using System;

namespace Assessment.Health.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}