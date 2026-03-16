using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.infrastruction
{
    public class ECommerceContext:IdentityDbContext<User>
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishList> Wishlists { get; set; }
        public DbSet<WishListItem> WishlistItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public ECommerceContext(DbContextOptions options):base(options)
        {
            
        }









        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().HasMany(o => o.Items).WithOne(it => it.Order).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WishList>().HasMany(w => w.Items).WithOne(it => it.Wishlist).HasForeignKey(it=>it.WishlistId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>().HasMany(p => p.Reviews).WithOne(r => r.Product).HasForeignKey(r=>r.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>().HasOne(u => u.Cart).WithOne(c => c.User).HasForeignKey<Cart>(c=>c.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasOne(u => u.Wishlist).WithOne(c => c.User).HasForeignKey<WishList>(w => w.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().HasMany(u => u.Addresses).WithOne(a => a.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);


            foreach(var prop in builder.Model.GetEntityTypes()
                .SelectMany(m=>m.GetProperties())
                .Where(p=>p.ClrType==typeof(decimal)||p.ClrType==typeof(decimal?)))
            {
                prop.SetPrecision(18);
                prop.SetScale(2);


            }

        }


    }
}
