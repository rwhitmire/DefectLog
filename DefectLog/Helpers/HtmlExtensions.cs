using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace System.Web
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString NavItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentController = routeData.GetRequiredString("controller");

            var link = htmlHelper.ActionLink(linkText, actionName, controllerName).ToString();
            var li = new TagBuilder("li");

            if (controllerName == currentController)
            {
                li.AddCssClass("active");
            }

            li.InnerHtml = link;

            return new MvcHtmlString(li.ToString());
        }

        public static MvcHtmlString NavItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentController = routeData.GetRequiredString("controller");

            var link = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToString();
            var li = new TagBuilder("li");

            if (controllerName == currentController)
            {
                li.AddCssClass("active");
            }
            else if (routeData.DataTokens["area"] as string == "Admin" && routeValues.ToString().Contains("{ area = Admin }"))
            {
                li.AddCssClass("active");
            }

            li.InnerHtml = link;

            return new MvcHtmlString(li.ToString());
        }

        public static MvcHtmlString ControllerListGroupItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string link;
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentController = routeData.GetRequiredString("controller");

            if (controllerName == currentController)
            {
                link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new {@class = "list-group-item active"}).ToString();
            }
            else
            {
                link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = "list-group-item" }).ToString();
            }

            return new MvcHtmlString(link);
        }

        public static MvcHtmlString ActionListGroupItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string link;
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");

            if (actionName == currentAction)
            {
                link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new {@class = "list-group-item active"}).ToString();
            }
            else
            {
                link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = "list-group-item" }).ToString();
            }

            return new MvcHtmlString(link);
        }
    }
}