using BusinessPortal.Data;
using BusinessPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.IRepository
{
    public class RequestRepository : IRepository<Request>
    {

        private readonly BusinessContext _db;

        public RequestRepository(BusinessContext db)
        {
            this._db = db;
        }
        public async Task Create(Request t)
        {
            _db.Requests.Add(t);
        }

        public async Task Delete(Request t)
        {
            _db.Requests.Remove(t);
        }

        public async Task<IEnumerable<Request>> GetAll()
        {
            return await _db.Requests.ToListAsync();
        }

        public async Task<Request> GetById(int id)
        {
            return await _db.Requests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Request t)
        {
            _db.Requests.Update(t);
        }
    }
}