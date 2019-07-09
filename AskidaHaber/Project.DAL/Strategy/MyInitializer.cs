using Bogus.DataSets;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Project.DAL.Strategy
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            AppUser ap = new AppUser();
            ap.UserName = "BerkeGzl";
            ap.Password = Crypto.HashPassword("Berke123");
            ap.Role = MODEL.Enums.Role.Admin;
            ap.IsActive = true;
            ap.FirstName = "Berke";
            ap.LastName = "Güzel";
            ap.Email = "berke.guzel96@gmail.com";
            ap.Gender = MODEL.Enums.Gender.Male;
            ap.AboutDescription = "Adminim Ben";
            ap.BirthDate = DateTime.Now;
            ap.ImagePath = new Images("en").People();
            ap.UserIP = new Internet("tr").Ip();
            context.AppUsers.Add(ap);
            context.SaveChanges();

            AppUser ap1 = new AppUser();
            ap1.UserName = "Admin";
            ap1.Password = Crypto.HashPassword("Admin123");
            ap1.Role = MODEL.Enums.Role.Admin;
            ap1.IsActive = true;
            ap1.FirstName = "Berke";
            ap1.LastName = "Güzel";
            ap1.Email = "admin@gmail.com";
            ap1.Gender = MODEL.Enums.Gender.Male;
            ap1.BirthDate = DateTime.Now;
            ap1.AboutDescription = "Adminim Ben";
            ap1.ImagePath = new Images("en").People();
            ap1.UserIP = new Internet("tr").Ip();
            context.AppUsers.Add(ap1);
            context.SaveChanges();

            AppUser ap2 = new AppUser();
            ap2.UserName = "AppUser";
            ap2.Password = Crypto.HashPassword("User123");
            ap2.Role = MODEL.Enums.Role.Member;
            ap2.IsActive = true;
            ap2.FirstName = "Berke";
            ap2.LastName = "Güzel";
            ap2.Email = "user@gmail.com";
            ap2.Gender = MODEL.Enums.Gender.Male;
            ap2.BirthDate = DateTime.Now;
            ap2.AboutDescription = "Kullanıcıyım Ben";
            ap2.ImagePath = new Images("en").People();
            ap2.UserIP = new Internet("tr").Ip();
            context.AppUsers.Add(ap2);
            context.SaveChanges();

            Editor e = new Editor();
            e.UserName = "Editor";
            e.Password = Crypto.HashPassword("Editor123");
            e.FirstName = "Berke";
            e.LastName = "Güzel";
            e.Email = "editor@gmail.com";
            e.Gender = MODEL.Enums.Gender.Male;
            e.AboutDescription = "Editörüm Ben";
            e.BirthDate = DateTime.Now;
            e.ImagePath = new Images("en").People();
            context.Editors.Add(e);
            context.SaveChanges();

            Columnist c = new Columnist();
            c.UserName = "Columnist";
            c.Password = Crypto.HashPassword("Columnist123");
            c.FirstName = "Berke";
            c.LastName = "Güzel";
            c.Email ="columnist@gmail.com";
            c.Gender = MODEL.Enums.Gender.Male;
            c.AboutDescription = "Yazarım Ben";
            c.BirthDate = DateTime.Now;
            c.ImagePath = new Images("en").People();
            context.Columnists.Add(c);
            context.SaveChanges();

            for (int i = 0; i < 11; i++)
            {
                AppUser app = new AppUser();
                Random rnd = new Random();
                app.UserName = new Internet("tr").UserName();
                app.Password = Crypto.HashPassword(new Internet("tr").Password());
                app.Role = MODEL.Enums.Role.Member;
                app.IsActive = true;
                app.FirstName = new Name("tr").FirstName();
                app.LastName = new Name("tr").LastName();
                app.Email = new Internet("tr").Email();
                app.UserIP = new Internet("tr").Ip();
                app.AboutDescription = new Lorem("tr").Sentence(5);
                app.Gender = new Commerce("tr").Random.Enum<MODEL.Enums.Gender>();
                app.BirthDate = DateTime.Now;
                switch (rnd.Next(1, 13))
                {
                    case 1:
                        app.ImagePath = new Images("en").Abstract();
                        break;
                    case 2:
                        app.ImagePath = new Images("en").Animals();
                        break;
                    case 3:
                        app.ImagePath = new Images("en").Business();
                        break;
                    case 4:
                        app.ImagePath = new Images("en").Cats();
                        break;
                    case 5:
                        app.ImagePath = new Images("en").City();
                        break;
                    case 6:
                        app.ImagePath = new Images("en").Food();
                        break;
                    case 7:
                        app.ImagePath = new Images("en").Nightlife();
                        break;
                    case 8:
                        app.ImagePath = new Images("en").Fashion();
                        break;
                    case 9:
                        app.ImagePath = new Images("en").People();
                        break;
                    case 10:
                        app.ImagePath = new Images("en").Nature();
                        break;
                    case 11:
                        app.ImagePath = new Images("en").Sports();
                        break;
                    case 12:
                        app.ImagePath = new Images("en").Technics();
                        break;
                    case 13:
                        app.ImagePath = new Images("en").Transport();
                        break;
                }
                context.AppUsers.Add(app);
                context.SaveChanges();
            }

            for (int i = 0; i < 11; i++)
            {
                AppUser app = new AppUser();
                Random rnd = new Random();
                app.UserName = new Internet("tr").UserName();
                app.Password = Crypto.HashPassword(new Internet("tr").Password());
                app.Role = MODEL.Enums.Role.Admin;
                app.IsActive = true;
                app.FirstName = new Name("tr").FirstName();
                app.LastName = new Name("tr").LastName();
                app.Email = new Internet("tr").Email();
                app.UserIP = new Internet("tr").Ip();
                app.AboutDescription = new Lorem("tr").Sentence(5);
                app.Gender = new Commerce("tr").Random.Enum<MODEL.Enums.Gender>();
                app.BirthDate = DateTime.Now;
                switch (rnd.Next(1, 13))
                {
                    case 1:
                        app.ImagePath = new Images("en").Abstract();
                        break;
                    case 2:
                        app.ImagePath = new Images("en").Animals();
                        break;
                    case 3:
                        app.ImagePath = new Images("en").Business();
                        break;
                    case 4:
                        app.ImagePath = new Images("en").Cats();
                        break;
                    case 5:
                        app.ImagePath = new Images("en").City();
                        break;
                    case 6:
                        app.ImagePath = new Images("en").Food();
                        break;
                    case 7:
                        app.ImagePath = new Images("en").Nightlife();
                        break;
                    case 8:
                        app.ImagePath = new Images("en").Fashion();
                        break;
                    case 9:
                        app.ImagePath = new Images("en").People();
                        break;
                    case 10:
                        app.ImagePath = new Images("en").Nature();
                        break;
                    case 11:
                        app.ImagePath = new Images("en").Sports();
                        break;
                    case 12:
                        app.ImagePath = new Images("en").Technics();
                        break;
                    case 13:
                        app.ImagePath = new Images("en").Transport();
                        break;
                }
                context.AppUsers.Add(ap);
                context.SaveChanges();
            }

            for (int i = 0; i < 11; i++)
            {
                Columnist cf = new Columnist();
                Random rnd = new Random();
                cf.UserName = new Internet("tr").UserName();
                cf.Password = Crypto.HashPassword(new Internet("tr").Password());
                cf.FirstName = new Name("tr").FirstName();
                cf.LastName = new Name("tr").LastName();
                cf.Email = new Internet("tr").Email();
                cf.AboutDescription = new Lorem("tr").Sentence(5);
                cf.Gender = new Commerce("tr").Random.Enum<MODEL.Enums.Gender>();
                cf.BirthDate = DateTime.Now;
                switch (rnd.Next(1, 13))
                {
                    case 1:
                        cf.ImagePath = new Images("en").Abstract();
                        break;
                    case 2:
                        cf.ImagePath = new Images("en").Animals();
                        break;
                    case 3:
                        cf.ImagePath = new Images("en").Business();
                        break;
                    case 4:
                        cf.ImagePath = new Images("en").Cats();
                        break;
                    case 5:
                        cf.ImagePath = new Images("en").City();
                        break;
                    case 6:
                        cf.ImagePath = new Images("en").Food();
                        break;
                    case 7:
                        cf.ImagePath = new Images("en").Nightlife();
                        break;
                    case 8:
                        cf.ImagePath = new Images("en").Fashion();
                        break;
                    case 9:
                        cf.ImagePath = new Images("en").People();
                        break;
                    case 10:
                        cf.ImagePath = new Images("en").Nature();
                        break;
                    case 11:
                        cf.ImagePath = new Images("en").Sports();
                        break;
                    case 12:
                        cf.ImagePath = new Images("en").Technics();
                        break;
                    case 13:
                        cf.ImagePath = new Images("en").Transport();
                        break;
                }
                context.Columnists.Add(cf);
                context.SaveChanges();
            }

            for (int i = 0; i < 11; i++)
            {
                Editor ef = new Editor();
                Random rnd = new Random();
                ef.UserName = new Internet("tr").UserName();
                ef.Password = Crypto.HashPassword(new Internet("tr").Password());
                ef.FirstName = new Name("tr").FirstName();
                ef.LastName = new Name("tr").LastName();
                ef.Email = new Internet("tr").Email();
                ef.AboutDescription = new Lorem("tr").Sentence(5);
                ef.Gender = new Commerce("tr").Random.Enum<MODEL.Enums.Gender>();
                ef.BirthDate = DateTime.Now;
                switch (rnd.Next(1, 13))
                {
                    case 1:
                        ef.ImagePath = new Images("en").Abstract();
                        break;
                    case 2:
                        ef.ImagePath = new Images("en").Animals();
                        break;
                    case 3:
                        ef.ImagePath = new Images("en").Business();
                        break;
                    case 4:
                        ef.ImagePath = new Images("en").Cats();
                        break;
                    case 5:
                        ef.ImagePath = new Images("en").City();
                        break;
                    case 6:
                        ef.ImagePath = new Images("en").Food();
                        break;
                    case 7:
                        ef.ImagePath = new Images("en").Nightlife();
                        break;
                    case 8:
                        ef.ImagePath = new Images("en").Fashion();
                        break;
                    case 9:
                        ef.ImagePath = new Images("en").People();
                        break;
                    case 10:
                        ef.ImagePath = new Images("en").Nature();
                        break;
                    case 11:
                        ef.ImagePath = new Images("en").Sports();
                        break;
                    case 12:
                        ef.ImagePath = new Images("en").Technics();
                        break;
                    case 13:
                        ef.ImagePath = new Images("en").Transport();
                        break;
                }
                context.Editors.Add(ef);
                context.SaveChanges();
            }

            List<Category> categories = new List<Category>
            {
                new Category{CategoryName = "Yazılar", Description= "Yazar Yazılar"},
                new Category{CategoryName = "SonDakika", Description= "Sondakika Haberler"},
                new Category{CategoryName = "Spor", Description= "Spor Haberleri"},
                new Category{CategoryName = "Magazin ", Description= "Magazin Haberleri"},
                new Category{CategoryName = "Ekonomi", Description= "Ekonomi Haberleri"},
                new Category{CategoryName = "Sağlık", Description= "Sağlık Haberleri"},
                new Category{CategoryName = "Seyehat", Description= "Seyehat Haberleri"},
                new Category{CategoryName = "Kültür", Description= "Kültür - Sanat Haberleri"},
                new Category{CategoryName = "Dünya", Description= "Dünya Haberleri"},
                new Category{CategoryName = "Gündem", Description= "Gündem Haberleri"},
                new Category{CategoryName = "Videolar", Description= "Video Listesi"}
            };
       
            foreach (Category item in categories)
            {
                for (int i = 0; i < 50; i++)
                {
                    News n = new News();
                    Random rnd = new Random();
                    n.Title = new Lorem("tr").Sentence(5);
                    n.Summary = new Lorem("tr").Sentence(5);
                    n.Content = new Lorem("tr").Sentence(5);
                    n.Quotation = new Lorem("tr").Sentence(5);
                    switch (rnd.Next(1, 13))
                    {
                        case 1:
                            n.ImagePath = new Images("en").Abstract();
                            break;
                        case 2:
                            n.ImagePath = new Images("en").Animals();
                            break;
                        case 3:
                            n.ImagePath = new Images("en").Business();
                            break;
                        case 4:
                            n.ImagePath = new Images("en").Cats();
                            break;
                        case 5:
                            n.ImagePath = new Images("en").City();
                            break;
                        case 6:
                            n.ImagePath = new Images("en").Food();
                            break;
                        case 7:
                            n.ImagePath = new Images("en").Nightlife();
                            break;
                        case 8:
                            n.ImagePath = new Images("en").Fashion();
                            break;
                        case 9:
                            n.ImagePath = new Images("en").People();
                            break;
                        case 10:
                            n.ImagePath = new Images("en").Nature();
                            break;
                        case 11:
                            n.ImagePath = new Images("en").Sports();
                            break;
                        case 12:
                            n.ImagePath = new Images("en").Technics();
                            break;
                        case 13:
                            n.ImagePath = new Images("en").Transport();
                            break;
                    }
                    if (item.CategoryName != "Yazılar" && item.CategoryName != "Videolar")
                    {
                        item.News.Add(n);
                    }
                }

                for (int k = 0; k < 50; k++)
                {
                    Article a = new Article();
                    Random rnd = new Random();
                    a.Title = new Lorem("tr").Sentence(5);
                    a.Summary = new Lorem("tr").Sentence(5);
                    a.Content = new Lorem("tr").Sentence(5);
                    a.Quotation = new Lorem("tr").Sentence(5);
                    a.ColumnistID = rnd.Next(1, 12);
                    switch (rnd.Next(1, 13))
                    {
                        case 1:
                            a.ImagePath = new Images("en").Abstract();
                            break;
                        case 2:
                            a.ImagePath = new Images("en").Animals();
                            break;
                        case 3:
                            a.ImagePath = new Images("en").Business();
                            break;
                        case 4:
                            a.ImagePath = new Images("en").Cats();
                            break;
                        case 5:
                            a.ImagePath = new Images("en").City();
                            break;
                        case 6:
                            a.ImagePath = new Images("en").Food();
                            break;
                        case 7:
                            a.ImagePath = new Images("en").Nightlife();
                            break;
                        case 8:
                            a.ImagePath = new Images("en").Fashion();
                            break;
                        case 9:
                            a.ImagePath = new Images("en").People();
                            break;
                        case 10:
                            a.ImagePath = new Images("en").Nature();
                            break;
                        case 11:
                            a.ImagePath = new Images("en").Sports();
                            break;
                        case 12:
                            a.ImagePath = new Images("en").Technics();
                            break;
                        case 13:
                            a.ImagePath = new Images("en").Transport();
                            break;
                    }
                    if (item.CategoryName == "Yazılar")
                    {
                        item.Articles.Add(a);
                    }     
                }
                context.Categories.Add(item);
                context.SaveChanges();
            }
        }
    }
}
