namespace RABC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Rcba : DbContext
    {
        public Rcba()
            : base("name=Rcba1")
        {
        }

        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Modules)
                .Map(m => m.ToTable("RoleModule").MapLeftKey("ModuleId").MapRightKey("RoleId"));

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);
        }
    }
}
