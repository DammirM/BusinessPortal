using static Azure.Core.HttpHeader;

namespace BusinessPortal.IRepository
{
    public interface IRepository<T>
    {

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T t);
        Task Update(T t);
        Task Delete(T t);
        Task Save();
    }
}
