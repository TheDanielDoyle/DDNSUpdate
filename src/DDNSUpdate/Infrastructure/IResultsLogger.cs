using FluentResults;

namespace DDNSUpdate.Infrastructure
{
    public interface IResultsLogger
    {
        void Log(Result result);
    }
}
