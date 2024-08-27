using ApiBookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBookStore.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public virtual DbSet<Translator> Translators { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books)
                .UsingEntity(j => j.ToTable("BookCategory"));

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Wishlists)
                .WithMany(w => w.Books)
                .UsingEntity(j => j.ToTable("WishlistBook"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("RoleUser"));

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Books)
                .WithOne(ci => ci.Cart)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Books)
                .WithOne(oi => oi.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); //Previene l'emliminazione a cascata degli utenti quando elimino un ordine

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithOne()
                .HasForeignKey<Order>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); //Previene l'emliminazione a cascata degli indirizzi quando elimino un ordine

        }
    }
}
