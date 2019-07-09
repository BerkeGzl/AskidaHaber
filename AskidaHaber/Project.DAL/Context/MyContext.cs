using Project.DAL.Strategy;
using Project.MAP.Options;
using Project.MODEL.Entities;
using System.Data.Entity;

namespace Project.DAL.Context
{
   public class MyContext : DbContext
    {
        public MyContext() : base ("AskiBaglantisi")
        {
            Configuration.ValidateOnSaveEnabled = false;

            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ColumnistMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new EditorMap());
            modelBuilder.Configurations.Add(new NewsMap());
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Columnist> Columnists { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Editor> Editors { get; set; }

        public DbSet<News> News { get; set; }
    }
}
