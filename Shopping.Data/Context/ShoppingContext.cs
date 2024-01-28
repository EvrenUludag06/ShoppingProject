using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Context
{
	public class ShoppingContext : DbContext
	{
		public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<UserEntity> Users => Set<UserEntity>();
		public DbSet<ProductEntity> Products => Set<ProductEntity>();
		public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
	}
}
