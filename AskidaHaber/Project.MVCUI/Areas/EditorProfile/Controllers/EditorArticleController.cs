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

namespace Project.MVCUI.Areas.EditorProfile.Controllers
{
    [RouteArea("editorprofile")]
    [Route("editor")]
    [EditorAuthentication]
    [ActFilter, ResFilter]
    public class EditorArticleController : Controller
    {
        public EditorArticleController()
        {
            article_repo = new ArticleRepository();
            cat_repo = new CategoryRepository();
            col_repo = new ColumnistRepository();
            com_repo = new CommentRepository();
        }
        ArticleRepository article_repo;
        CategoryRepository cat_repo;
        ColumnistRepository col_repo;
        CommentRepository com_repo;
        // GET: EditorProfile/EditorArticle
        [Route("yazi-listesi")]
        public ActionResult ListArticle()
        {
            return View(article_repo.SelectActives());
        }

        [Route("yazi-guncelle/{id:int}")]
        public ActionResult UpdateArticle(int id)
        {
            List<Category> kategori = cat_repo.Where(x => x.ID == 1);
            return View(Tuple.Create(article_repo.GetByID(id), col_repo.SelectActives() ,kategori));
        }

        [Route("yazi-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateArticle([Bind(Prefix =("Item1"))] Article item, HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            item.ModifiedBy = (Session["editor"] as Editor).UserName;
            article_repo.Update(item);
            return RedirectToAction("ListArticle");
        }

        [Route("yazi-detayi/{id:int}")]
        public ActionResult DetailArticle(int id)
        {
            List<Comment> yakalananYorum = com_repo.Where(x => x.ArticleID == id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(article_repo.GetByID(id),yakalananYorum));
        }

        [Route("yazi-guncelle/{cid:int}-{aid:int}-{caid:int}")]
        public ActionResult DeleteArticleComment(int cid, int aid, int caid)
        {
            com_repo.SpecialDelete(cid);
            return RedirectToAction("DetailsArticle", new { area = "", controller = "Member", category = Url.FriendlyURLTitle(article_repo.GetByID(aid).Category.CategoryName), Title = Url.FriendlyURLTitle(article_repo.GetByID(aid).Title), id = aid, catID = caid });
        }

        [Route("yazi-yorum-sil/{idd:int}-{aid:int}")]
        public ActionResult DeleteCommentArticle(int idd, int aid)
        {
            com_repo.SpecialDelete(idd);
            return RedirectToAction("DetailArticle", new { id = aid });
        }

        [Route("silinmis-yazi-listesi")]
        public ActionResult DeletedsArticle()
        {
            return View(article_repo.SelectDeleteds());
        }

    }
}