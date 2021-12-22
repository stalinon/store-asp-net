using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Find(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,                    // фильтрация данных Where
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,   // установка порядка вывода объектов
            string includeProperties = null,                            // включение параметров
            bool isTracking = true                                      // отслеживание запроса
            );

        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,                    // фильтрация данных Where
            string includeProperties = null,                            // включение параметров
            bool isTracking = true                                      // отслеживание запроса
            );

        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}
