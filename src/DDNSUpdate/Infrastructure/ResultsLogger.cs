using System;
using System.Collections.Generic;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Infrastructure;

public class ResultsLogger : IResultsLogger
{
    private readonly IDictionary<Type, Action<IReason>> _logFactory;

    public ResultsLogger(ILoggerFactory loggerFactory)
    {
        ILogger logger = loggerFactory.CreateLogger<ResultsLogger>();
        _logFactory = new Dictionary<Type, Action<IReason>>
        {
            { typeof(Error), (r => logger.LogError(r.Message)) },
            { typeof(Success), (r => logger.LogInformation(r.Message)) }
        };
    }

    public void Log(Result result)
    {
        foreach (IReason reason in result.Reasons)
        {
            _logFactory[reason.GetType()]?.Invoke(reason);
        }
    }
}