using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class ViewResultDiagnostics : IActionFilter
    {
        private readonly IFilterDiagnostics _filterDiagnostics;

        public ViewResultDiagnostics(IFilterDiagnostics filterDiagnostics)
        {
            _filterDiagnostics = filterDiagnostics;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                _filterDiagnostics.AddMessage($"View Name: {viewResult.ViewName}");
                _filterDiagnostics.AddMessage($"Model Type: {viewResult.Model.GetType().Name}");
            }
        }
    }
}
