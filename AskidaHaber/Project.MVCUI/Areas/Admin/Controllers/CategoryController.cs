using PagedList;
using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.MODEL.Entities;
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
    public class CategoryController : Controller
    {    
        public CategoryController()
        {
            cat_repo = new CategoryRepository();
        }
        CategoryRepository cat_repo;


        // GET: Admin/Category
        [Route("kategori-listesi")]
        public ActionResult ListCategory()
        {
            return View(cat_repo.SelectActives());
        }

        [Route("kategori-ekle")]
        public ActionResult AddCategory()
        {
            return View();
        }

        [Route("kategori-ekle")]
        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
            if (cat_repo.Any(x => x.CategoryName == item.CategoryName))
            {
                ViewBag.Mevcut = "Kategori Zaten Mevcut";
            }
            else
            {
                item.CreatedBy = (Session["admin"] as AppUser).UserName;
                cat_repo.Add(item);
                return RedirectToAction("ListCategory");
            }
            return View();
        }

        [Route("kategori-guncelle/{id:int}")]
        public ActionResult UpdateCategory(int id)
        {
            return View(cat_repo.GetByID(id));
        }

        [Route("kategori-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateCategory(Category item)
        {
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            cat_repo.Update(item);
            return RedirectToAction("ListCategory");
        }

        [Route("kategori-sil/{id:int}")]
        public ActionResult DeleteCategory(int id)
        {
            cat_repo.Delete(cat_repo.GetByID(id));
            return RedirectToAction("ListCategory");
        }

        [Route("silinmis-kategori-listesi")]
        public ActionResult DeletedsCategory()
        {
            return View(cat_repo.SelectDeleteds());
        }

        [Route("kategori-s-sil/{id:int}")]
        public ActionResult SpecialDeleteCategory(int id)
        {
            cat_repo.SpecialDelete(id);
            return RedirectToAction("ListCategory");
        }
    }
}