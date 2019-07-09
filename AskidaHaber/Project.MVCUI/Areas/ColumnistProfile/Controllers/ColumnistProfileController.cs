using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.ColumnistProfile.Controllers
{
    [RouteArea("columnistprofile")]
    [Route("yazar")]
    [ColumnistAuthentication]
    [ActFilter, ResFilter]
    public class ColumnistProfileController : Controller
    {
        public ColumnistProfileController()
        {
            col_repo = new ColumnistRepository();
            article_repo = new ArticleRepository();
        }
        ColumnistRepository col_repo;
        ArticleRepository article_repo;

        // GET: ColumnistProfile/ColumnistProfile
        [Route("profil/{id:int}")]
        public ActionResult ColumnistProfile(int id)
        {
            return View(col_repo.GetByID(id));
        }
        [Route("profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(col_repo.GetByID(id));
        }

        [Route("profil-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateProfile(Columnist item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            col_repo.Update(item);
            return RedirectToAction("ColumnistProfile", new { id = (Session["columnist"] as Columnist).ID });
        }

        [Route("sifre-guncelle")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("sifre-guncelle")]
        [HttpPost]
        public ActionResult UpdatePassword(Columnist item)
        {
            Columnist guncellenen = col_repo.GetByID((Session["columnist"] as Columnist).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            col_repo.Update(guncellenen);
            return RedirectToAction("ColumnistProfile", new { id = (Session["columnist"] as Columnist).ID });
        }
    }
}