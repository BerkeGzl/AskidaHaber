using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.Models.Filters;
using Project.TOOLUI.MyTools;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [ActFilter, ResFilter]
    public class HomeController : Controller
    {
        public HomeController()
        {
            App_repo = new AppUserRepository();
            Editor_repo = new EditorRepository();
            Columnist_repo = new ColumnistRepository();
        }
        AppUserRepository App_repo;
        EditorRepository Editor_repo;
        ColumnistRepository Columnist_repo;

        private void RememberMe(AppUser item, string Remember)
        {
            if (Remember == "true") //cookie varmı yokmu kontrol ediliyor.
            {
                HttpCookie loginName = new HttpCookie("userName"); //yeni cookie olusturduk.
                loginName.Expires = DateTime.Now.AddMinutes(5);
                loginName.Value = item.UserName;
                //cookie eklenmesi için Response'lere ekleme yapıyoruz.
                Response.Cookies.Add(loginName);
                HttpCookie loginPassword = new HttpCookie("password");
                loginPassword.Expires = DateTime.Now.AddMinutes(5);
                loginPassword.Value = item.Password;
                Response.Cookies.Add(loginPassword);
            }
        }

        //cookie'de saklanana değerleri yakalamak icin request property'sini kullanıyoruz.
        private AppUser CheckCookie()
        {
            string userName = string.Empty, password = string.Empty;
            AppUser cookieStored = null;
            if (Request.Cookies["userName"] != null && Request.Cookies["password"] != null)
            {
                userName = Request.Cookies["userName"].Value;
                password = Request.Cookies["password"].Value;
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                cookieStored = new AppUser
                {
                    UserName = userName,
                    Password = password
                };
            }
            return cookieStored;
        }

        // GET: Home
        [Route("giris/kullanici-giris")]
        public ActionResult Login()
        {
            AppUser enters = CheckCookie();
            if (enters == null) return View();
            return View(enters);
        }

        [Route("giris/kullanici-giris")]
        [HttpPost]
        public ActionResult Login(AppUser item, string Remember)
        {
                if (App_repo.Any(x => x.UserName == item.UserName && x.Role == Role.Admin && x.Status != DataStatus.Deleted))
                {
                    AppUser girenKisi = App_repo.Default(x => x.UserName == item.UserName && x.Role == Role.Admin);
                        if (girenKisi.IsBanned == true)
                        {
                            ViewBag.Banli = "Banlandın!";
                            return View();
                        }
                        bool result = Crypto.VerifyHashedPassword(girenKisi.Password, item.Password);
                    if (result)
                    {
                        RememberMe(item, Remember);
                        Session["admin"] = girenKisi;
                        return RedirectToAction("NewsList", "Member");
                    }
                }
                else if (App_repo.Any(x => x.UserName == item.UserName && x.Role == Role.Member))
                {
                    AppUser girenUye = App_repo.Default(x => x.UserName == item.UserName);
                        if (girenUye.IsBanned == true)
                        {
                            ViewBag.Banli = "Banlandın!";
                            return View();
                        }
                        bool result = Crypto.VerifyHashedPassword(girenUye.Password, item.Password);
                    if (result)
                    {
                        RememberMe(item, Remember);
                        Session["member"] = girenUye;
                        return RedirectToAction("NewsList", "Member");
                    }
                }
                else if (Editor_repo.Any(x => x.UserName == item.UserName))
                {
                    Editor girenEditor = Editor_repo.Default(x => x.UserName == item.UserName);
                        if (girenEditor.IsBanned == true)
                        {
                            ViewBag.Banli = "Banlandın!";
                            return View();
                        }
                        bool result = Crypto.VerifyHashedPassword(girenEditor.Password, item.Password);
                    if (result)
                    {
                        RememberMe(item, Remember);
                        Session["editor"] = girenEditor;
                        return RedirectToAction("NewsList", "Member");
                    }
                }
                else if (Columnist_repo.Any(x => x.UserName == item.UserName))
                {
                    Columnist girenYazar = Columnist_repo.Default(x => x.UserName == item.UserName);
                         if (girenYazar.IsBanned == true)
                         {
                             ViewBag.Banli = "Banlandın!";
                             return View();
                         }
                     bool result = Crypto.VerifyHashedPassword(girenYazar.Password, item.Password);
                    if (result)
                    {
                        RememberMe(item, Remember);
                        Session["columnist"] = girenYazar;
                        return RedirectToAction("NewsList", "Member");
                    }
                }
                ViewBag.Message = "Hatalı kullanıcı adı veya şifre";
                return View();
        }

        [Route("giris/kullanici-kayit")]
        public ActionResult Register(Guid? id)
        {
            //id null değilse buraya düsecek
            if (id != null)
            {
                //Aktivasyon kodları eşleşiyorsa buraya düsecek.
                if (App_repo.Any(x => x.ActivationCode == id))
                {
                    //Hesap aktifleştirilecek.
                    AppUser ToBeActive = Session["register"] as AppUser;
                    ToBeActive.IsActive = true;
                    App_repo.Update(ToBeActive);
                    TempData["Basarili"] = "Hesabınızı Başarıyla Aktif Ettiniz!";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.UndefinedUser = "Kullanıcı girişi yapmadınız";
                }
            }
            //Session null değilse ama id null ise buraya düşecek.
            else if (Session["register"] != null && id == null)
            {
                //Hesap aktif değil ise buraya düşecek.
                if ((Session["register"] as AppUser).IsActive == false)
                {
                    ViewBag.NotActive = "Hesabınızı aktif etmemişsiniz";
                }
            }
            return View();
        }

        [Route("giris/kullanici-kayit")]
        [HttpPost]
        public ActionResult Register(AppUser item)
        {
            //Username ve Mail adresi veritabanında var mı yok mu diye sorgulanıyor. Varsa buraya düşecek.
            if (App_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || Editor_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email) || Columnist_repo.Any(x => x.UserName == item.UserName || x.Email == item.Email))
            {
                ViewBag.Kayitli = "Böyle bir kullanıcı zaten mevcut";
                return View();
            }
            item.ImagePath = "";
            //Veritabanında yoksa buraya düşecek ve hesap aktivasyonu gönderilecek.
            string SendMessage = "Hesabınız Oluşturuldu. Aktif etmek için http://localhost:63265/Home/Register/" + item.ActivationCode + " linkine tıklayabilirsiniz.";
            MailSender.Send(item.Email, password: "Berke123", body: SendMessage, subject: "Hoşgeldiniz!");
            item.Password = Crypto.HashPassword(item.Password);
            item.UserIP = Request.UserHostAddress;
            App_repo.Add(item);
            Session["register"] = App_repo.GetByID(item.ID); //kullanıcı Register olduğu anda kullanıcı için session açılıyor.
            return RedirectToAction("RegisterOK");
        }

        [Route("giris/kayit-ok")]
        public ActionResult RegisterOk()
        {
            return View();
        }

        [Route("giris/sifremi-unuttum")]
        public ActionResult IForgot() 
        {
            return View();
        }

        [Route("giris/sifremi-unuttum")]
        [HttpPost]
        public ActionResult IForgot(AppUser item)
        {
            if (App_repo.Any(x => x.UserName == item.UserName && x.Email == item.Email))
            {
                Session["yeniSifre"] = App_repo.Default(x => x.UserName == item.UserName && x.Email == item.Email);
                string sendMessage = "Şifre sıfırlamak için http://localhost:63265/Home/NewPassword/" + item.ResetPassword + " linkine tıklayabilirsiniz.";
                MailSender.Send(item.Email, body: sendMessage);
                return RedirectToAction("ResetOk");
            }
            return View();
        }

        [Route("giris/sifirlama-ok")]
        public ActionResult ResetOk()
        {
            return View();
        }

        [Route("giris/yeni-sifre")]
        public ActionResult NewPassword(Guid? id)
        {
            return View();
        }

        [Route("giris/yeni-sifre")]
        [HttpPost]
        public ActionResult NewPassword(AppUser item)
        {
            AppUser guncellenecek = Session["yeniSifre"] as AppUser;
            guncellenecek.Password = Crypto.HashPassword(item.Password);
          App_repo.Update(guncellenecek);

            return RedirectToAction("NewsList", "Member");
        }

        [Route("giris/cikis")]
        public ActionResult LogOut()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
            }
            else if (Session["member"] != null)
            {
                Session.Remove("member");
            }
            else if (Session["editor"] != null)
            {
                Session.Remove("editor");
            }
            else if (Session["columnist"] != null)
            {
                Session.Remove("columnist");
            }
            return RedirectToAction("NewsList", "Member");
        }
    }
}