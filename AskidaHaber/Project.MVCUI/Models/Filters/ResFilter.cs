using Project.BLL.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System.Web;
using System.Web.Mvc;
using static Project.MODEL.Entities.Log;

namespace Project.MVCUI.Models.Filters
{
    public class ResFilter : FilterAttribute, IResultFilter
    {
        public ResFilter()
        {
            user = new Log();
        }
        Log user;

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LoglamaYap(filterContext, false);
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            LoglamaYap(filterContext, true);
        }

        public void LoglamaYap(ControllerContext filterContext, bool girisMi)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            if (filterContext.HttpContext.Session["member"] == null && filterContext.HttpContext.Session["admin"] == null && filterContext.HttpContext.Session["editor"] == null && filterContext.HttpContext.Session["columnist"] == null)
            {
                user.WhoIs = "Anonim";
            }
            else if (filterContext.HttpContext.Session["member"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
                user.IPAdress = (filterContext.HttpContext.Session["member"] as AppUser).UserIP;
            }
            else if (filterContext.HttpContext.Session["admin"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
                user.IPAdress = (filterContext.HttpContext.Session["admin"] as AppUser).UserIP;
            }
            else if (filterContext.HttpContext.Session["editor"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["editor"] as Editor).UserName;
                user.IPAdress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            }
            else if (filterContext.HttpContext.Session["columnist"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["columnist"] as Columnist).UserName;
                user.IPAdress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            }

            switch (girisMi)
            {
                case false:
                    user.ActionName = filterContext.RouteData.Values["Action"].ToString();
                    user.ControllorName = filterContext.RouteData.Values["Controller"].ToString();
                    user.Information = "View Çalıştıktan Sonra Kaydedildi.";
                    user.Description = Keyword.Exit;
                    break;
                case true:
                    user.ActionName = filterContext.RouteData.Values["Action"].ToString();
                    user.ControllorName = filterContext.RouteData.Values["Controller"].ToString();
                    user.Information = "View Çalışmadan Önce Kaydedildi.";
                    user.Description = Keyword.Enter;
                    break;
            }

            user.UrlAccessed = request.RawUrl;
            using (LogContext logDb = new LogContext())
            {
                logDb.Logs.Add(user);
                logDb.SaveChanges();
            }
        }
    }
}