using System.Web;
using System.Web.Routing;

namespace Portal
{
    public sealed class ControllerLessRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new ControllerLessHttpHandler(requestContext);
        }
    }
}