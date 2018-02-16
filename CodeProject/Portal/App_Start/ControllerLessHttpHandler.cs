using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal
{
    public class ControllerLessHttpHandler : IHttpHandler
    {
        /// <summary>
        /// The request context.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        /// The configuration settings.
        /// </summary>
        private readonly RouteConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerLessHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The requestContext to be used in this instance.</param>
        public ControllerLessHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
            _configuration = RouteConfiguration.GetConfigurationSettings();

            var target = typeof(ControllerLessHttpHandler).Namespace;
            if (!ControllerBuilder.Current.DefaultNamespaces.Contains(target))
            {
                ControllerBuilder.Current.DefaultNamespaces.Add(target);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this class is reusable.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var controller = _requestContext.RouteData.GetRequiredString("controller");
            var action = String.Empty;

            if (_requestContext.RouteData.Values["action"] != null)
            {
                action = _requestContext.RouteData.Values["action"].ToString();
            }

            if (!String.IsNullOrEmpty(action))
            {
                IController viewController = null;
                IControllerFactory controllerFactory = null;
                try
                {
                    controllerFactory = ControllerBuilder.Current.GetControllerFactory();
                    try
                    {
                        viewController = controllerFactory.CreateController(_requestContext, controller);
                        viewController.Execute(_requestContext);
                    }
                    catch (Exception ex)
                    {
                        DispatchRequest(controllerFactory, controller, action);
                    }
                }
                finally
                {
                    if (controllerFactory != null)
                    {
                        controllerFactory.ReleaseController(viewController);
                    }
                }
            }
        }

        /// <summary>
        /// Dispatches the request.
        /// </summary>
        /// <param name="controllerFactory">The controller factory.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        private void DispatchRequest(IControllerFactory controllerFactory, string controller, string action)
        {
            string currentRoute = _requestContext.HttpContext.Request.CurrentExecutionFilePath;
            string defaultPage = System.Configuration.ConfigurationManager.AppSettings["DefaultPage"].ToString();

            _requestContext.HttpContext.Response.Redirect(defaultPage + "?referral" + currentRoute + "&" + "CurrentRoute=" + currentRoute);
        }
    }
}