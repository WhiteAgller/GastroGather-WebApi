

using Common.Interfaces;

namespace Common.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}