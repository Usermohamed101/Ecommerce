using Ecommerce.infrastruction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Helper
{
    public static class Seeder
    {

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            var roles = new[] { "User", "Admin" };
            foreach(var role in roles)
            {
              if (!await roleManager.RoleExistsAsync("User"))
                {
               var res= await roleManager.CreateAsync(new IdentityRole("User"));

                    if (!res.Succeeded) throw new Exception($"{role} role couldnt be added");
                }
              

            }
          

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));


            var adminEmail = "admin@admin.com";
            var adminPass = "Admin1234!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new User() { FName = "admin", LName = "admin", Email = adminEmail, UserName = adminEmail };
                var res=  await userManager.CreateAsync(admin, adminPass);
                if (!res.Succeeded) throw new Exception($"{admin} account couldnt be added");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

        }


        public static async Task SeedCategoriesAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<ECommerceContext>();
            var categories = service.Set<Category>();

            if(!await categories.AnyAsync())
            {

                categories.AddRangeAsync(
                    
                    new Category() { Name="Electronics"},new Category() {Name="Clothes" }
                    ,new Category() { Name="Eatables"}                    
                    
                    
                    );
                var res = await service.SaveChangesAsync();
                if (res == 0)
                    throw new Exception("categories couldnt be added");
            }
         
            
        }

        public static async Task SeedProductsAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<ECommerceContext>();
            var products = service.Set<Product>();

            if (!await products.AnyAsync())
            {

                products.AddRangeAsync(

                    new Product() { Name = "Laptop", Price = 10224, Description = "laptop with great capabilities", StockQuantity = 11, CategoryId = 1 },
                    new Product() { Name = "Laptop", Price = 10224, Description = "laptop with great capabilities", StockQuantity = 11, CategoryId = 1 },
                    new Product() { Name = "Laptop", Price = 10224, Description = "laptop with great capabilities", StockQuantity = 11, CategoryId = 1 }

                    );

                var res = await service.SaveChangesAsync();
                if (res == 0) throw new Exception("products couldnt be added");
            }



        }

        public static async Task SeedDeliveryOptionsAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<ECommerceContext>();
            var products = service.Set<DeliveryOption>();

            if (!await products.AnyAsync())
            {

                products.AddRangeAsync(

                    new DeliveryOption() { Name = "Standard Delivery", DeliveryPrice = 5, Description = "delivery in 3-5 days", EstimatedDeliveryDays = 5, },
                     new DeliveryOption() { Name = "Express Delivery", DeliveryPrice = 15, Description = "delivery in 1-2 days", EstimatedDeliveryDays = 2, },
                      new DeliveryOption() { Name = "Same-Day Delivery", DeliveryPrice = 25, Description = "delivery on the same day", EstimatedDeliveryDays = 0, },
                       new DeliveryOption() { Name = "Store Pickup", DeliveryPrice = 0, Description = "pick up from store in 24 hours", EstimatedDeliveryDays = 1, }


                    );

                var res = await service.SaveChangesAsync();
                if (res == 0) throw new Exception("Delivery options couldnt be added");
            }



        }

    }
}
