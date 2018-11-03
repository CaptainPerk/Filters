using Filters.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Linq;
using Xunit;

namespace Filters_Tests.Infrastructure
{
    public class HttpOnlyAttributeTests
    {
        [Fact(DisplayName = "HttpsOnlyAttribute => When called with a non-Https request a 'Forbidden' status code is returned")]
        public void HttpsOnlyReturns403WhenNotHttps()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.SetupSequence(hr => hr.IsHttps).Returns(true).Returns(false);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(hc => hc.Request).Returns(mockHttpRequest.Object);

            var actionContext = new ActionContext(mockHttpContext.Object, new RouteData(), new ActionDescriptor());
            var authorizationFilterContext = new AuthorizationFilterContext(actionContext, Enumerable.Empty<IFilterMetadata>().ToList());

            var httpsOnlyAttribute = new HttpsOnlyAttribute();

            // Act & Assert
            httpsOnlyAttribute.OnAuthorization(authorizationFilterContext);
            Assert.Null(authorizationFilterContext.Result);

            httpsOnlyAttribute.OnAuthorization(authorizationFilterContext);
            Assert.IsType<StatusCodeResult>(authorizationFilterContext.Result);
            Assert.Equal(StatusCodes.Status403Forbidden, ((StatusCodeResult) authorizationFilterContext.Result).StatusCode);
        }
    }
}
