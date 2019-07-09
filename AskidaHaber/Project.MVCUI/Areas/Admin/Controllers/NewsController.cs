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
    public class NewsController : Controller
    {
        public NewsController()
        {
            news_repo = new NewsRepository();
            cat_repo = new CategoryRepository();
            com_repo = new CommentRepository();
        }
        NewsRepository news_repo;
        CategoryRepository cat_repo;
        CommentRepository com_repo;
        //GET: Admin/News
        [Route("haber-listesi")]
        public ActionResult ListNews(int sayfa = 1)
        {
            return View(news_repo.SelectActives());
        }

        [Route("haber-ekle")]
        public ActionResult AddNews()
        {
            List<Category> kategori = cat_repo.Where(x => x.ID != 1 && x.ID != 11);
            return View(Tuple.Create(new News(), kategori));
        }

        [Route("haber-ekle")]
        [HttpPost]
        public ActionResult AddNews([Bind(Prefix ="Item1")]News item, HttpPostedFileBase resim, HttpPostedFileBase video)
        {
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            item.VideoPath = VideoUploader.UploadVideo("~/Videos", video);
            news_repo.Add(item);
            return RedirectToAction("ListNews");
        }

        [Route("haber-guncelle/{id:int}")]
        public ActionResult UpdateNews(int id)
        {
            List<Category> kategori = cat_repo.Where(x => x.ID != 1);
            return View(Tuple.Create(news_repo.GetByID(id), kategori));
        }

        [Route("haber-guncelle/{id:int}")]
        [HttpPost]
        public ActionResult UpdateNews([Bind(Prefix ="Item1")] News item, HttpPostedFileBase resim, HttpPostedFileBase video)
        {
            if (resim != null)
            {
                item.ImagePath = ImageUploader.UploadImage("~/Pictures", resim);
            }
            if (video != null)
            {
                item.VideoPath = VideoUploader.UploadVideo("~/Videos", video);
            }
            item.ModifiedBy = (Session["admin"] as AppUser).UserName;
            news_repo.Update(item);
            return RedirectToAction("ListNews");
        }

        [Route("haber-sil/{id:int}")]
        public ActionResult DeleteNews(int id)
        {
            news_repo.Delete(news_repo.GetByID(id));
            return RedirectToAction("ListNews");
        }

        [Route("silinmis-haber-listesi")]
        public ActionResult DeletedsNews(int sayfa = 1)
        {
            return View(news_repo.SelectDeleteds());
        }

        [Route("haber-detayi/{id:int}")]
        public ActionResult DetailNews(int id)
        {
            List<Comment> yakalananYorum = com_repo.Where(x => x.NewsID == id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(news_repo.GetByID(id), yakalananYorum));
        }

        [Route("haber-s-sil/{id:int}")]
        public ActionResult SpecialDeleteNews(int id)
        {
            news_repo.SpecialDelete(id);
            return RedirectToAction("ListNews");
        }

        [Route("haber-yorum-sil/{idd:int}-{aid:int}")]
        public ActionResult DeleteCommentNews(int idd, int aid)
        {
            com_repo.SpecialDelete(idd);
            return RedirectToAction("DetailNews", new { id = aid });
        }

        [Route("haber-yorum-sil/{cid:int}-{aid:int}-{caid:int}")]
        public ActionResult DeleteNewsComment(int cid, int aid, int caid)
        {
            com_repo.SpecialDelete(cid);
            return RedirectToAction("DetailsNews", new { area = "", controller = "Member", category = Url.FriendlyURLTitle(news_repo.GetByID(aid).Category.CategoryName), Title = Url.FriendlyURLTitle(news_repo.GetByID(aid).Title), id = aid, catID = caid });
        }
    }
}