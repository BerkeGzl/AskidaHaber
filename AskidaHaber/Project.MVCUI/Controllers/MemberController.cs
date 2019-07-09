using PagedList;
using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [ActFilter, ResFilter]
    public class MemberController : Controller
    {
        public MemberController()
        {
            news_repo = new NewsRepository();
            cat_repo = new CategoryRepository();
            col_repo = new ColumnistRepository();
            article_repo = new ArticleRepository();
            nvm = new NewsVM();
            comment_repo = new CommentRepository();
        }
        NewsRepository news_repo;
        CategoryRepository cat_repo;
        ColumnistRepository col_repo;
        ArticleRepository article_repo;
        NewsVM nvm;
        CommentRepository comment_repo;

        // GET: Member  
        [Route("")]
        [Route("haberler")]
        [Route("haberler/tum-haberler")]
        public ActionResult NewsList(int? page)
        {
            nvm.karisik = news_repo.Where(x => x.CategoryID != 4 && x.CategoryID != 5 && x.CategoryID != 6 && x.CategoryID != 2 && x.CategoryID != 10 && x.CategoryID != 9 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            nvm.magazin = news_repo.Where(x => x.CategoryID == 4 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            nvm.ekonomi = news_repo.Where(x => x.CategoryID == 5 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            nvm.saglik = 
            nvm.sonDakika = news_repo.Where(x => x.CategoryID == 2 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            nvm.gundem = news_repo.Where(x => x.CategoryID == 10 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            nvm.dunya = news_repo.Where(x => x.CategoryID == 9 && x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            return View(nvm);
        }

        [Route("yazılar/tum-yazılar")]
        public ActionResult ArticleList(int sayfa = 1)
        {
            return View(Tuple.Create(article_repo.Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5), col_repo.SelectActives()));
        }

        [Route("hakkımızda")]
        public ActionResult AboutUs()
        {
            return View();
        }

        [Route("yazarlar")]
        public ActionResult ListColumnist(int sayfa = 1)
        {
            return View(Tuple.Create(article_repo.Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5), col_repo.SelectActives()));
        }

        [Route("yazılar/{category}/{Title}-{id:int}-{catID:int}")]
        public ActionResult DetailsArticle(int id, int catID)
        {
            List<Comment> yakalananYorum = comment_repo.Where(x => x.ArticleID == id).OrderByDescending(x => x.CreatedDate).ToList();
            List<Article> yakalananKategori = article_repo.Where(x => x.CategoryID == catID && x.ID != id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(article_repo.GetByID(id), yakalananKategori, yakalananYorum, new Comment()));
        }

        [HttpPost]
        public ActionResult AppUserArticleComment([Bind(Prefix = "Item1")] Article item, [Bind(Prefix = "Item4")] Comment item2)
        {
            if (Session["member"] != null)
            {
                item2.AppUserID = (Session["member"] as AppUser).ID;
            }
            else if (Session["admin"] != null)
            {
                item2.AppUserID = (Session["admin"] as AppUser).ID;
            }
            item2.ArticleID = item.ID;
            comment_repo.Add(item2);
            return RedirectToAction("DetailsArticle", new { category = Url.FriendlyURLTitle(item.Category.CategoryName), Title = Url.FriendlyURLTitle(item.Title), id = item.ID, catID = item.CategoryID });
        }

        [Route("haberler/{category}/{Title}-{id:int}-{catID:int}")]
        public ActionResult DetailsNews(int id, int catID)
        {
            List<News> yakalananKategori = news_repo.Where(x => x.CategoryID == catID && x.ID != id).OrderByDescending(x => x.CreatedDate).ToList();
            List<Comment> yakalananYorum = comment_repo.Where(x => x.NewsID == id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(news_repo.GetByID(id), yakalananKategori, yakalananYorum, new Comment()));
        }

        [HttpPost]
        public ActionResult AppUserNewsComment([Bind(Prefix = "Item1")] News item, [Bind(Prefix = "Item4")] Comment item2)
        {
            if (Session["member"] != null)
            {
                item2.AppUserID = (Session["member"] as AppUser).ID;
            }
            else if (Session["admin"] != null)
            {
                item2.AppUserID = (Session["admin"] as AppUser).ID;
            }
            item2.NewsID = item.ID;
            comment_repo.Add(item2);
            return RedirectToAction("DetailsNews", new { category = Url.FriendlyURLTitle(item.Category.CategoryName), Title = Url.FriendlyURLTitle(item.Title), id = item.ID, catID = item.CategoryID });
        }

        [Route("yazarlar/{FirstName}/{LastName}-{id:int}")]
        public ActionResult DetailsColumnist(int id, int sayfa = 1)
        {
            return View(Tuple.Create(article_repo.Where(x => x.ColumnistID == id && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5), article_repo.SelectActives(), col_repo.SelectActives()));
        }

        [Route("haberler/{category = Kategoriler}/{id:int}")]
        public ActionResult SelectbyCategory(int id, int sayfa = 1)
        {
            List<News> karisik = news_repo.Where(x => x.CategoryID != id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(Tuple.Create(news_repo.Where(x => x.Status != DataStatus.Deleted && x.CategoryID == id).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5), cat_repo.GetByID(id), karisik));
        }

        public ActionResult SonDakika()
        {
            List<News> dakika = news_repo.Where(x => x.CategoryID == 2 && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
            return PartialView("_SonDakika", dakika);
        }

        [Route("haberler/tum-videolar")]
        public ActionResult ListVideo(int sayfa = 1)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Member/_VideoList.cshtml", news_repo.Where(x => x.VideoPath != null && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5));
            }         
            return View(news_repo.Where(x => x.VideoPath != null && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(sayfa, 5));
        }

        [Route("arama")]
        public ActionResult Search(string result, int? page, int? page2)
        {
            ViewBag.Keyword = result;
            ViewBag.Keyword2 = result;
            return View(Tuple.Create(news_repo.Where(x => x.Title.Contains(result) || x.Summary.Contains(result) || x.Content.Contains(result) && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(page ?? 1, 3), article_repo.Where(x => x.Title.Contains(result) || x.Summary.Contains(result) || x.Content.Contains(result) && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(page2 ?? 1, 3), news_repo.Where(x => x.CategoryID == 6 && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList(),news_repo.Where(x => x.CategoryID == 9 && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList()));
        }

        public PartialViewResult PartialSearch(string result, int? page, string result2, int? page2)
        {
            ViewBag.Keyword = result;
            ViewBag.Keyword2 = result2;
            return PartialView("_SearchResult", Tuple.Create(news_repo.Where(x => x.Title.Contains(result) || x.Summary.Contains(result) || x.Content.Contains(result) && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(page ?? 1, 3), article_repo.Where(x => x.Title.Contains(result2) || x.Summary.Contains(result2) || x.Content.Contains(result2) && x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToPagedList(page2 ?? 1, 3)));
        }
    }
}