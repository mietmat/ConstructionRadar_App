using ConstructionRadar_App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionRadar_App.Data
{
    public class ConstructionRadarDbContext : DbContext
    {
     

        public ConstructionRadarDbContext(DbContextOptions<ConstructionRadarDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();


    }
}
