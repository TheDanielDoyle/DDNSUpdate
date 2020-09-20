using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        public static Task WithAggregatedExceptions(this Task task)
        {
            /*
            Massive thanks to NoseRatio for the below code.
            Found here :- https://stackoverflow.com/questions/12007781/why-doesnt-await-on-task-whenall-throw-an-aggregateexception/62607500#62607500
            */
            return task.ContinueWith(
                continuationFunction: anteTask =>
                    anteTask.IsFaulted &&
                    anteTask.Exception is AggregateException ex &&
                    (ex.InnerExceptions.Count > 1 || ex.InnerException is AggregateException) ?
                    Task.FromException(ex.Flatten()) : anteTask,
                cancellationToken: CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler: TaskScheduler.Default).Unwrap();
        }
    }
}
