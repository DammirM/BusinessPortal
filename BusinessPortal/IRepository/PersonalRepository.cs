using BusinessPortal.Data;
using BusinessPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BusinessPortal.IRepository
{
    public class PersonalRepository : IRepository<Personal>
    {
        private readonly BusinessContext _db;

        public PersonalRepository(BusinessContext db)
        {
            this._db = db;
        }
        public async Task Create(Personal t)
        {
            _db.Personals.Add(t);
        }

        public async Task Delete(Personal t)
        {
            _db.Personals.Remove(t);
        }

        public async Task<IEnumerable<Personal>> GetAll()
        {
            return await _db.Personals.ToListAsync();
        }

        public async Task<Personal> GetById(int id)
        {
            return await _db.Personals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Personal t)
        {
            _db.Personals.Update(t);
        }
    }
}
