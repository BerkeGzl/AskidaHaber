using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.BLL.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.BLL.RepositoryPattern.RepositoryConcrete
{
   public class LogRepository : BaseRepository<Log>
    {
        protected LogContext ldb;
        public LogRepository()
        {
            ldb = DBTool2.DBLogInstance;
        }

        public override void Add(Log item)
        {
            ldb.Set<Log>().Add(item);
            Save();
        }

        public override bool Any(Expression<Func<Log, bool>> exp)
        {
            return ldb.Set<Log>().Any(exp);
        }

        public override void Delete(Log item)
        {
            item.Status = DataStatus.Deleted;
            Save();
        }

        public override Log GetByID(int id)
        {
            return ldb.Set<Log>().Find(id);
        }

        public override int GetLastAdded()
        {
            return ldb.Set<Log>().OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }

        public override object ListAnonymous(Expression<Func<Log, object>> exp)
        {
            return ldb.Set<Log>().Select(exp).ToList();
        }

        protected override void Save()
        {
            ldb.SaveChanges();
        }

        public override List<Log> SelectActives()
        {
            return ldb.Set<Log>().Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public override List<Log> SelectAll()
        {
            return ldb.Set<Log>().ToList();
        }

        public override List<Log> SelectDeleteds()
        {
            return ldb.Set<Log>().Where(x => x.Status == DataStatus.Deleted).ToList();
        }

        public override List<Log> SelectModifieds()
        {
            return ldb.Set<Log>().Where(x => x.Status == DataStatus.Updated).ToList();
        }

        public override void SpecialDelete(int id)
        {
            ldb.Set<Log>().Remove(GetByID(id));
            Save();
        }

        public override void Update(Log item)
        {
            item.Status = DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            Log ToBeUpdated = GetByID(item.ID);
            ldb.Entry(ToBeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public override List<Log> Where(Expression<Func<Log, bool>> exp)
        {
            return ldb.Set<Log>().Where(exp).ToList();
        }

        public override Log Default(Expression<Func<Log, bool>> exp)
        {
            return ldb.Set<Log>().FirstOrDefault(exp);
        }
    }
}
