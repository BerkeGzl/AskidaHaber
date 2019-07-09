using PagedList;
using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Enums;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using System.Linq;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    [ActFilter, ResFilter]
    public class LogController : Controller
    {
        public LogController()
        {
            log_repo = new LogRepository();
        }
        LogRepository log_repo;

        [Route("gunluk-listesi")]
        public ActionResult LogList()
        {
            return View(log_repo.SelectActives());
        }
    }
}