using Project.MAP.Options;
using Project.MODEL.Entities;
using System.Data.Entity;

namespace Project.DAL.Context
{
    public class LogContext : DbContext
    {
        public LogContext() : base ("AskiBaglantisiLog") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogMap());
        }

        public DbSet<Log> Logs { get; set; }
    }
}
