using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Project.BLL.RepositoryPattern.RepositoryInterface
{
    public interface IRepository<T> where T: BaseEntity
    {
        void Add(T item);

        void Update(T item);

        void Delete(T item);

        void SpecialDelete(int id);

        T GetByID(int id);

        T Default(Expression<Func<T, bool>> exp);
        int GetLastAdded();

        List<T> SelectAll();

        List<T> SelectActives();

        List<T> SelectDeleteds();

        List<T> SelectModifieds();

        bool Any(Expression<Func<T, bool>> exp);

        object ListAnonymous(Expression<Func<T, object>> exp);

        List<T> Where(Expression<Func<T, bool>> exp);
    }
}
