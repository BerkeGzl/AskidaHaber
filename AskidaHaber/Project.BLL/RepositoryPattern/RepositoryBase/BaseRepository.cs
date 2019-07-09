using Project.BLL.RepositoryPattern.RepositoryInterface;
using Project.BLL.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.BLL.RepositoryPattern.RepositoryBase
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MyContext db;
        public BaseRepository()
        {
            db = DBTool.DBInstance;
        }

        /// <summary>
        /// Gelen veriyi veritabanına ekler.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            db.Set<T>().Add(item);
            Save();
        }

        /// <summary>
        /// Gelen veri varmı yokmu onu cevaplar.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
        }


        /// <summary>
        /// Yalandan veri siler.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(T item)
        {
            item.Status = DataStatus.Deleted;
            Save();
        }

        /// <summary>
        /// İstenilen id deki verileri getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetByID(int id)
        {
            return db.Set<T>().Find(id);
        }

        /// <summary>
        /// Eklenen son veriyi getirir.
        /// </summary>
        /// <returns></returns>
        public virtual int GetLastAdded()
        {
            return db.Set<T>().OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }

        /// <summary>
        /// İstenilen sütunlara göre anonim bir liste desteği sağlar.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual object ListAnonymous(Expression<Func<T, object>> exp)
        {
            return db.Set<T>().Select(exp).ToList();
        }

        /// <summary>
        /// Yapılan işlemleri kaydeder.
        /// </summary>
        protected virtual void Save()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Aktif statüsündeki verileri getirir.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> SelectActives()
        {
            return db.Set<T>().Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
        }

        /// <summary>
        /// Her şeyi listeler.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> SelectAll()
        {
            return db.Set<T>().ToList();
        }

        /// <summary>
        /// Silinmiş statüsündeki verileri getirir.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> SelectDeleteds()
        {
            return db.Set<T>().Where(x => x.Status == DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
        }
        
        /// <summary>
        /// Güncellenmis statüsündeki verileri getirir.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> SelectModifieds()
        {
            return db.Set<T>().Where(x => x.Status == DataStatus.Updated).OrderByDescending(x => x.CreatedDate).ToList();
        }

        /// <summary>
        /// Gerçekten siler.
        /// </summary>
        /// <param name="id"></param>
        public virtual void SpecialDelete(int id)
        {
            db.Set<T>().Remove(GetByID(id));
            Save();
        }

        /// <summary>
        /// Eski veriyi yenisiyle günceller.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(T item)
        {
            item.Status = DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            T ToBeUpdated = GetByID(item.ID);
            db.Entry(ToBeUpdated).CurrentValues.SetValues(item);
            Save();
        }


        /// <summary>
        /// Bildiğimiz Where sorgusu.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual List<T> Where(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Where(exp).ToList();
        }

        public virtual T Default(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().FirstOrDefault(exp);
        }
    }
}
