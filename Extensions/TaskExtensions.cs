using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BigRedCloud.Api.Extensions
{
    internal static class TaskExtensions
    {
        public static void WaitAndUnwrapException(this Task task)
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                ThrowInitialException(ex);
                throw; // Never reach it
            }
        }

        public static TResult ResultAndUnwrapException<TResult>(this Task<TResult> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                ThrowInitialException(ex);
                throw; // Never reach it
            }
        }

        private static void ThrowInitialException(AggregateException ex)
        {
            var initialException = ex.Flatten().InnerExceptions.First();
            ExceptionDispatchInfo.Capture(initialException).Throw();
        }
    }
}
