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
    public class EditorController : Controller
    {
        public EditorController()
        {
            col_repo = new ColumnistRepository();
            app_repo = new AppUserRepository();
            editor_repo = new EditorRepository();
        }

        ColumnistRepository col_repo;
        AppUserRepository app_repo;
        EditorRepository editor_repo;

        // GET: Admin/Editor
        [Route("editor-listesi")]
        public ActionResult ListEditor()
        {
            return View(editor_repo.SelectActives());
        }

        [Route("editor-ekle")]
        public ActionResult AddEditor()
        {
            return View();
        }

        [Route("editor-ekle")]
        [HttpPost]
        public ActionResult AddEditor(Editor item, HttpPostedFileBase resim)
        {
            if (col_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || editor_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || app_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email))
            {
                ViewBag.Mevcut = "Böyle bir kullanıcı mevcut";
                return View();
            }
            if (item.ImagePath != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.Password = Crypto.HashPassword(item.Password);
            editor_repo.Add(item);
            return RedirectToAction("ListEditor");
        }

        [Route("editor-guncelle/{id:int}")]
        public ActionResult UpdateEditor(int id)
        {
            return View(editor_repo.GetByID(id));
        }

        [Route("editor-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateEditor(Editor item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures/Users", resim);
            }
            if (editor_repo.Any(x => x.Password != item.Password))
            {
                item.Password = Crypto.HashPassword(item.Password);
            }
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            editor_repo.Update(item);
            return RedirectToAction("ListEditor");
        }

        [Route("editor-sil/{id:int}")]
        public ActionResult DeleteEditor(int id)
        {
            editor_repo.Delete(editor_repo.GetByID(id));
            return RedirectToAction("ListEditor");
        }

        [Route("silinmis-editor-listesi")]
        public ActionResult DeletedsEditor()
        {
            return View(editor_repo.SelectDeleteds());
        }

        [Route("banlı-editor-listesi")]
        public ActionResult BansEditor()
        {
            return View(editor_repo.SelectBanUsers());
        }

        [Route("editor-s-sil/{id:int}")]
        public ActionResult SpecialDeleteEditor(int id)
        {
            editor_repo.SpecialDelete(id);
            return RedirectToAction("ListEditor");
        }

        [Route("editor-sifresi-guncelle/{id:int}")]
        public ActionResult UpdateEditorPassword(int id)
        {
            Session["gelenEditor"] = editor_repo.GetByID(id);
            return View();
        }

        [Route("editor-sifresi-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateEditorPassword(Editor item)
        {
            Editor guncellenen = editor_repo.GetByID((Session["gelenEditor"] as Editor).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            guncellenen.ModifiedBy = (Session["admin"] as AppUser).UserName;
            editor_repo.Update(guncellenen);
            return RedirectToAction("ListEditor");
        }
    }
}