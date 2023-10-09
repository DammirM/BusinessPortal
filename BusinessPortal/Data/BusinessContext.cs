using BusinessPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Data
{
    public class BusinessContext : DbContext
    {

        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
        {

        }

        public DbSet<Personal> Personals { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }


    }
}
