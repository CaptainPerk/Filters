using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;

namespace Filters.Infrastructure
{
    public class ViewResultDetailsAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var dictionary = new Dictionary<string, string>{ ["Result Type"] = context.Result.GetType().Name };

            if (context.Result is ViewResult viewResult)
            {
                dictionary["View Name"] = viewResult.ViewName;
                dictionary["Model Type"] = viewResult.Model.GetType().Name;
                dictionary["Model Data"] = viewResult.Model.ToString();
            }

            context.Result = new ViewResult
            {
                ViewName = "Message",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = dictionary}
            };
        }
    }
}
