using PagedList;
using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    [ActFilter, ResFilter]
    public class ColumnistController : Controller
    {
        public ColumnistController()
        {
            col_repo = new ColumnistRepository();
            app_repo = new AppUserRepository();
            editor_repo = new EditorRepository();
        }
        ColumnistRepository col_repo;
        AppUserRepository app_repo;
        EditorRepository editor_repo;

        // GET: Admin/Columnist
        [Route("yazar-listesi")]
        public ActionResult ListColumnist()
        {
            return View(col_repo.SelectActives());
        }

        [Route("yazar-ekle")]
        public ActionResult AddColumnist()
        {
            return View();
        }

        [Route("yazar-ekle")]
        [HttpPost]
        public ActionResult AddColumnist(Columnist item, HttpPostedFileBase resim)
        {
            if (col_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || editor_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || app_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email))
            {
                ViewBag.Mevcut = "Böyle bir kullanıcı mevcut";
                return View();
            }
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.ImagePath = ImageUploader.UploadImage("~/Pictures/Users", resim);
            item.Password = Crypto.HashPassword(item.Password);
            col_repo.Add(item);
            return RedirectToAction("ListColumnist");
        }

        [Route("yazar-guncelle/{id:int}")]
        public ActionResult UpdateColumnist(int id)
        {
            return View(col_repo.GetByID(id));
        }

        [Route("yazar-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateColumnist(Columnist item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures/Users", resim);
            }
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            item.Password = Crypto.HashPassword(item.Password);
            col_repo.Update(item);
            return RedirectToAction("ListColumnist");
        }

        [Route("yazar-sil/{id:int}")]
        public ActionResult DeleteColumnist(int id)
        {
            col_repo.Delete(col_repo.GetByID(id));
            return RedirectToAction("ListColumnist");
        }

        [Route("banli-yazar-listesi")]
        public ActionResult BansColumnist(int sayfa = 1)
        {
            return View(col_repo.SelectBanUsers());
        }

        [Route("silinmis-yazar-listesi")]
        public ActionResult DeletedsColumnist(int sayfa = 1)
        {
            return View(col_repo.SelectDeleteds());
        }

        [Route("yazar-s-sil/{id:int}")]
        public ActionResult SpecialDeleteColumnist(int id)
        {
            editor_repo.SpecialDelete(id);
            return RedirectToAction("ListColumnist");
        }

        [Route("yazar-sifresi-guncelle/{id:int}")]
        public ActionResult UpdateColumnistPassword(int id)
        {
            Session["gelenYazar"] = col_repo.GetByID(id);
            return View();
        }

        [Route("yazar-sifresi-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateColumnistPassword(Columnist item)
        {
            Columnist guncellenen = col_repo.GetByID((Session["gelenYazar"] as Columnist).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            guncellenen.ModifiedBy = (Session["admin"] as AppUser).UserName;
            col_repo.Update(guncellenen);
            return RedirectToAction("ListColumnist");
        }
    }
}