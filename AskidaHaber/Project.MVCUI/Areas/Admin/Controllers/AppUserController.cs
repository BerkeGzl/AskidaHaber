using PagedList;
using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System;
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
    public class AppUserController : Controller
    {
        public AppUserController()
        {
            app_repo = new AppUserRepository();
            editor_repo = new EditorRepository();
            columnist_repo = new ColumnistRepository();
        }
        AppUserRepository app_repo;
        ColumnistRepository columnist_repo;
        EditorRepository editor_repo;

        // GET: Admin/AppUser
        [Route("kullanici-listesi")]
        public ActionResult ListAppUser(int sayfa = 1)
        {         
            return View(app_repo.SelectActives());
        }

        [Route("kullanici-ekle")]
        public ActionResult AddAppUser()
        {
            return View();
        }

        [Route("kullanici-ekle")]
        [HttpPost]
        public ActionResult AddAppUser(AppUser item, HttpPostedFileBase resim)
        {
            if (app_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || editor_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || columnist_repo.Any(x => x.Email == item.Email))
            {
                ViewBag.Kayitli = "Böyle bir kullanıcı zaten mevcut";
                return View();
            }
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            item.Password = Crypto.HashPassword(item.Password);
            item.UserIP = Request.UserHostAddress;
            app_repo.Add(item);
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-guncelle/{id:int}")]
        public ActionResult UpdateAppUser(int id)
        {
            return View(app_repo.GetByID(id));
        }

        [Route("kullanici-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateAppUser(AppUser item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            app_repo.Update(item);
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-sifre-guncelleme/{id:int}")]
        public ActionResult UpdateAppUserPassword(int id)
        {
            Session["gelenKullanici"] = app_repo.GetByID(id);
            return View();
        }

        [Route("kullanici-sifre-guncelleme/{id:int}")]
        [HttpPost]
        public ActionResult UpdateAppUserPassword(AppUser item)
        {
            AppUser guncellenen = app_repo.GetByID((Session["gelenKullanici"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            guncellenen.ModifiedBy = (Session["admin"] as AppUser).UserName;
            app_repo.Update(guncellenen);
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-sil/{id:int}")]
        public ActionResult DeleteAppUser(int id)
        {
            app_repo.Delete(app_repo.GetByID(id));
            return RedirectToAction("ListAppUser");
        }

        [Route("banli-kullanici-listesi")]
        public ActionResult BansAppUser(int sayfa = 1)
        {
            return View(app_repo.SelectBanUsers());
        }

        [Route("silinmis-kullanici-listesi")]
        public ActionResult DeletedsUsers(int sayfa = 1)
        {
            return View(app_repo.SelectDeleteds());
        }

        [Route("kullanici-s-sil/{id:int}")]
        public ActionResult SpecialDeleteAppUser(int id)
        {
            app_repo.SpecialDelete(id);
            return RedirectToAction("ListAppUser");
        }
    }
}