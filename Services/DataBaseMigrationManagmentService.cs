using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public static class DataBaseMigrationManagmentService
    {
        public static void initImigration(IServiceProvider service)
        {
            service.GetRequiredService<ECommerceContext>().Database.Migrate();
        }

    }
}
