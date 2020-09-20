using Microsoft.Extensions.Logging;
using System;

namespace DDNSUpdate.Tests
{
    public class FakeLogger<T> : ILogger<T>, IDisposable
    {
        public static readonly FakeLogger<T> Instance = new FakeLogger<T>();

        private FakeLogger()
        {

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new FakeLogger<T>();
        }

        public void Dispose()
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}
