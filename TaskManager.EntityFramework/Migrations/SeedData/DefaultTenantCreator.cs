using System.Linq;
using TaskManager.EntityFramework;
using TaskManager.MultiTenancy;

namespace TaskManager.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly TaskManagerDbContext _context;

        public DefaultTenantCreator(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = "Default", Name = "Default"});
                _context.SaveChanges();
            }
        }
    }
}