using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Entities.Concrete
{
    public class EnterpriseMagnetDbContext : DbContext
    {

        public EnterpriseMagnetDbContext(DbContextOptions<EnterpriseMagnetDbContext> options) : base(options)
        {

        }

        #region Tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
