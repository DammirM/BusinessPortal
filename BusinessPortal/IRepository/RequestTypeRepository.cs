using BusinessPortal.Data;
using BusinessPortal.Models;

namespace BusinessPortal.IRepository
{
    public class RequestTypeRepository : IRepository<RequestType>
    {

        private readonly BusinessContext _db;

        public RequestTypeRepository(BusinessContext db)
        {
            this._db= db;
        }
        public Task Create(RequestType t)
        {
            throw new NotImplementedException();
        }

        public Task Delete(RequestType t)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RequestType>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RequestType> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(RequestType t)
        {
            throw new NotImplementedException();
        }
    }
}
