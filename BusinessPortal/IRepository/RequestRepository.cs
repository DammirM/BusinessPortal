using BusinessPortal.Data;
using BusinessPortal.Models;

namespace BusinessPortal.IRepository
{
    public class RequestRepository : IRepository<Request>
    {

        private readonly BusinessContext _db;

        public RequestRepository(BusinessContext db)
        {
            this._db = db;
        }
        public Task Create(Request t)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Request t)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Request>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(Request t)
        {
            throw new NotImplementedException();
        }
    }
}
