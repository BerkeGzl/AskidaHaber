using System.Web.Mvc;

namespace Project.MVCUI.Areas.ColumnistProfile
{
    public class ColumnistProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ColumnistProfile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();
            context.MapRoute(
                "ColumnistProfile_default",
                "ColumnistProfile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}