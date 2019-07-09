using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.MemberProfile.Controllers
{
    [RouteArea("memberprofile")]
    [Route("kullanici")]
    [MemberAuthentication]
    [ActFilter, ResFilter]
    public class MemberProfileController : Controller
    {
        // GET: MemberProfile/MemberProfile
        public MemberProfileController()
        {
            app_repo = new AppUserRepository();
        }
        AppUserRepository app_repo;

        // GET: Member/MemberProfile
        [Route("profil/{id:int}")]
        public ActionResult MemberProfile(int id)
        {
            return View(app_repo.GetByID(id));
        }

        [Route("profil-guncelleme/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(app_repo.GetByID(id));
        }

        [Route("profil-guncelleme/{id:int}")]
        [HttpPost]
        public ActionResult UpdateProfile(AppUser item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            app_repo.Update(item);
            return RedirectToAction("MemberProfile", new { id = (Session["member"] as AppUser).ID });
        }

        [Route("sifre-guncelleme")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("sifre-guncelleme")]
        [HttpPost]
        public ActionResult UpdatePassword(AppUser item)
        {
            AppUser guncellenen = app_repo.GetByID((Session["member"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            app_repo.Update(guncellenen);   
            return RedirectToAction("MemberProfile", new { id = (Session["member"] as AppUser).ID });
        }
    }
}