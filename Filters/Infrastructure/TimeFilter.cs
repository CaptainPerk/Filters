using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class TimeFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private readonly IFilterDiagnostics _filterDiagnostics;
        private Stopwatch stopwatch;

        public TimeFilter(IFilterDiagnostics filterDiagnostics)
        {
            _filterDiagnostics = filterDiagnostics;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            stopwatch = Stopwatch.StartNew();
            await next();
            _filterDiagnostics.AddMessage($"Action Time: {stopwatch.Elapsed.TotalMilliseconds}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
            stopwatch.Stop();
            _filterDiagnostics.AddMessage($"Result Time: {stopwatch.Elapsed.TotalMilliseconds}");
        }
    }
}
