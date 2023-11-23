using System.Collections.Generic;

namespace Sachy_Obrazky.Repository
{
    public interface IRepository<T, in TId>
    {
        List<T> GetAll();
        void Create(T item);
        void Delete(TId id);
        void Update(T item);
        T Find(TId id);
        T Find(T item);

    }
}