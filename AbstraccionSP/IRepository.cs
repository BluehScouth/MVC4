using System.Collections.Generic;

namespace EDOSwit.AbstraccionSP
{
    public interface IRepository<T> where T : class
    {
        IList<T> Get(params object[] parameters);
        IList<T> GetPaged(params object[] parameters);
        T Single(params object[] parameters);
        int Count(params object[] parameters);
        T Add(T entity);
        T Update(T entity);
        void Delete(params object[] parameters);
    }
}