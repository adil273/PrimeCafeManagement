using Microsoft.EntityFrameworkCore;

namespace PrimeCafeManagement.Models
{
    public class PrimeCafeContext:DbContext
    {
        public PrimeCafeContext(DbContextOptions option):base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuStatus> MenuStatuses { get; set; }
        public DbSet<MenuTitle> MenuTitles { get; set; }
        public DbSet<MenuPrice> MenuPrices { get; set; }
    }
}
