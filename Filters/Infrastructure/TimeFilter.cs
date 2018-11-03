using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class TimeFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private ConcurrentQueue<double> actionTimes = new ConcurrentQueue<double>();
        private ConcurrentQueue<double> resultTimes = new ConcurrentQueue<double>();
        private readonly IFilterDiagnostics _filterDiagnostics;

        public TimeFilter(IFilterDiagnostics filterDiagnostics)
        {
            _filterDiagnostics = filterDiagnostics;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next();
            stopwatch.Stop();
            actionTimes.Enqueue(stopwatch.Elapsed.TotalMilliseconds);
            _filterDiagnostics.AddMessage($"Action Time: {stopwatch.Elapsed.TotalMilliseconds} Average: {actionTimes.Average():F2}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next();
            stopwatch.Stop();
            resultTimes.Enqueue(stopwatch.Elapsed.TotalMilliseconds);
            _filterDiagnostics.AddMessage($"Result Time: {stopwatch.Elapsed.TotalMilliseconds} Average: {resultTimes.Average():F2}");
        }
    }
}
