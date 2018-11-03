using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class DiagnosticsFilter : IAsyncResultFilter
    {
        private readonly IFilterDiagnostics _filterDiagnostics;

        public DiagnosticsFilter(IFilterDiagnostics filterDiagnostics)
        {
            _filterDiagnostics = filterDiagnostics;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();

            foreach (var message in _filterDiagnostics.Messages)
            {
                byte[] bytes = Encoding.ASCII.GetBytes($"<div>{message}</div>");
                await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
