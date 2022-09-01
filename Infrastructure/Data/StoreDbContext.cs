using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreDbContext:DbContext
    {
		public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
		{

		}
		public DbSet<Product> Products{get;set;}
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            //builder.Property(p => p.ProductBrand).WithMany().
            //  HasForeignKey(p => p.ProductBrandId);
            //builder.Property(p => p.ProductType).WithMany().
            //  HasForeignKey(p => p.ProductBrandId);
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}