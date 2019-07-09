using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.EditorProfile.Controllers
{
    [RouteArea("editorprofile")]
    [Route("editor")]
    [EditorAuthentication]
    [ActFilter, ResFilter]
    public class EditorProfileController : Controller
    {
        public EditorProfileController()
        {
            editor_repo = new EditorRepository();
        }
        EditorRepository editor_repo;

        // GET: EditorProfile/EditorProfile
        [Route("anasayfa")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("profil/{id:int}")]
        public ActionResult EditorProfile(int id)
        {
            return View(editor_repo.GetByID(id));
        }

        [Route("profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(editor_repo.GetByID(id));
        }

        [Route("profil-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateProfile(Editor item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            editor_repo.Update(item);
            return RedirectToAction("EditorProfile", new { id = (Session["editor"] as Editor).ID });
        }

        [Route("sifre-guncelle")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("sifre-guncelle")]
        [HttpPost]
        public ActionResult UpdatePassword(Editor item)
        {
            Editor guncellenen = editor_repo.GetByID((Session["editor"] as Editor).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            editor_repo.Update(guncellenen);
            return RedirectToAction("EditorProfile", new { id = (Session["editor"] as Editor).ID });
        }
    }
}