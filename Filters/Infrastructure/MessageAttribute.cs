using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Filters.Infrastructure
{
    public class MessageAttribute : ResultFilterAttribute
    {
        private readonly string _message;

        public MessageAttribute(string message)
        {
            _message = message;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            WriteMessage(context, $"<div>Before Result: {_message}");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            WriteMessage(context, $"<div>After Result: {_message}");
        }

        private static void WriteMessage(FilterContext context, string message)
        {
            var bytes = Encoding.ASCII.GetBytes(message);
            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
        }
    }
}
