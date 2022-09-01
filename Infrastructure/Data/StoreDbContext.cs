using Core.Entities;
using Microsoft.EntityFrameworkCore; 
namespace Infrastructure.Data
{
	public class StoreDbContext:DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
		{

		}
		public DbSet<Product> Products{get;set;}
	}
}