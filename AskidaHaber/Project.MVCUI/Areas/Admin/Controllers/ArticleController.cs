using PagedList;
using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    [ActFilter, ResFilter]
    public class ArticleController : Controller
    {
        public ArticleController()
        {
            article_repo = new ArticleRepository();
            col_repo = new ColumnistRepository();
            cat_repo = new CategoryRepository();
            edit_repo = new EditorRepository();
            com_repo = new CommentRepository();
        }
        ArticleRepository article_repo;
        ColumnistRepository col_repo;
        CategoryRepository cat_repo;
        EditorRepository edit_repo;
        CommentRepository com_repo;

        [Route("yazi-listesi")]
        // GET: Admin/Article
        public ActionResult ListArticle()
        {
            return View(article_repo.SelectActives());
        }

        [Route("yazi-ekle")]
        public ActionResult AddArticle()
        {
            List<Category> kategori = cat_repo.Where(x => x.ID == 1);
            return View(Tuple.Create(new Article(), col_repo.SelectActives(), kategori));
        }

        [Route("yazi-ekle")]
        [HttpPost]
        public ActionResult AddArticle([Bind(Prefix ="Item1")] Article item, HttpPostedFileBase resim)
        {
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            article_repo.Add(item);
            return RedirectToAction("ListArticle");
        }

        [Route("yazi-guncelle/{id:int}")]
        public ActionResult UpdateArticle(int id)
        {
            List<Category> kategori = cat_repo.Where(x => x.ID == 1);
            return View(Tuple.Create(article_repo.GetByID(id), col_repo.SelectActives(), kategori));
        }

        [Route("yazi-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateArticle([Bind(Prefix ="Item1")] Article item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            article_repo.Update(item);
            return RedirectToAction("ListArticle");
        }

        [Route("yazi-sil/{id:int}")]
        public ActionResult DeleteArticle(int id)
        {
            article_repo.Delete(article_repo.GetByID(id));
            return RedirectToAction("ListArticle");
        }

        [Route("silinmis-yazi-listesi")]
        public ActionResult DeletedsArticle(int sayfa = 1)
        {
            return View(article_repo.SelectDeleteds());
        }

        [Route("yazi-detayi/{id:int}")]
        public ActionResult DetailArticle(int id)
        {
            List<Comment> yakalananYorum = com_repo.Where(x => x.ArticleID == id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(article_repo.GetByID(id), yakalananYorum));
        }

        [Route("yazi-s-sil/{id:int}")]
        public ActionResult SpecialDeleteArticle(int id)
        {
            article_repo.SpecialDelete(id);
            return RedirectToAction("ListArticle");
        }

        [Route("yazi-yorum-sil/{idd:int}-{aid:int}")]
        public ActionResult DeleteCommentArticle(int idd, int aid)
        {
            com_repo.SpecialDelete(idd);
            return RedirectToAction("DetailArticle", new {id = aid});
        }

        [Route("yazi-yorum-sil/{cid:int}-{aid:int}-{caid:int}")]
        public ActionResult DeleteArticleComment(int cid, int aid, int caid)
        {
            com_repo.SpecialDelete(cid);
            return RedirectToAction("DetailsArticle", new {area ="", controller="Member", category = Url.FriendlyURLTitle(article_repo.GetByID(aid).Category.CategoryName), Title = Url.FriendlyURLTitle(article_repo.GetByID(aid).Title), id = aid, catID = caid });
        }

    }
}