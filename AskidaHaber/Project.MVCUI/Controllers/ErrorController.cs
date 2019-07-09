using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        // GET: Error
        [Route("hata/sayfa-bulunamadi")]
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}