using TaskManager.EntityFramework;
using EntityFramework.DynamicFilters;

namespace TaskManager.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly TaskManagerDbContext _context;

        public InitialHostDbBuilder(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
