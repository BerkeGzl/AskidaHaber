using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    [ActFilter, ResFilter]
    public class AdminHomeController : Controller
    {
        public AdminHomeController()
        {
            app_repo = new AppUserRepository();
        }
        AppUserRepository app_repo;

        // GET: Admin/AdminHome
        [Route("anasayfa")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("profil/{id:int}")]
        public ActionResult AdminProfile(int id)
        {
            return View(app_repo.GetByID(id));
        }

        [Route("profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(app_repo.GetByID(id));
        }

        [Route("profil-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateProfile(AppUser item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            app_repo.Update(item);
            return RedirectToAction("AdminProfile", new {id = (Session["admin"] as AppUser).ID});
        }

        [Route("profil-sifre-guncelle/{id:int}")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("profil-sifre-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdatePassword(AppUser item)
        {
            AppUser guncellenen = app_repo.GetByID((Session["admin"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            app_repo.Update(guncellenen);
            return RedirectToAction("AdminProfile", new { id = (Session["admin"] as AppUser).ID });
        }
    }
}