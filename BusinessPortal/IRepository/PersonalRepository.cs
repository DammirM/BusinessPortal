using BusinessPortal.Data;
using BusinessPortal.Models;

namespace BusinessPortal.IRepository
{
    public class PersonalRepository : IRepository<Personal>
    {
        private readonly BusinessContext _db;

        public PersonalRepository(BusinessContext db)
        {
            this._db = db;
        }
        public Task Create(Personal t)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Personal t)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Personal>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Personal> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(Personal t)
        {
            throw new NotImplementedException();
        }
    }
}
