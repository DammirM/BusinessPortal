using BusinessPortal.Data;
using BusinessPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.IRepository
{
    public class RequestTypeRepository : IRepository<RequestType>
    {

        private readonly BusinessContext _db;

        public RequestTypeRepository(BusinessContext db)
        {
            this._db = db;
        }
        public async Task Create(RequestType t)
        {
            await _db.AddAsync(t);
        }

        public async Task Delete(RequestType t)
        {
            _db.Remove(t);
        }

        public async Task<IEnumerable<RequestType>> GetAll()
        {
            return await _db.RequestTypes.ToListAsync();
        }

        public async Task<RequestType> GetById(int id)
        {
            return await _db.RequestTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(RequestType t)
        {
            _db.Update(t);
        }
    }
}
