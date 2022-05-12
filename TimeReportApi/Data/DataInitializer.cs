using Microsoft.EntityFrameworkCore;

namespace TimeReportApi.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext context;

        public DataInitializer(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void SeedData()
        {
            context.Database.Migrate();
        }
    }
}
