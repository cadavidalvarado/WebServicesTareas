using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebServiceTarea.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.UserName).HasColumnName("NombreUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.Email).HasColumnName("Email");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.EmailConfirmed).HasColumnName("ConfirmacionEmail");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PasswordHash).HasColumnName("Contrasena");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.SecurityStamp).HasColumnName("Seguridad");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PhoneNumber).HasColumnName("Telefono");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PhoneNumberConfirmed).HasColumnName("ConfirmacionTelefono");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.TwoFactorEnabled).HasColumnName("FactoresActivacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.LockoutEndDateUtc).HasColumnName("FechaBloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.LockoutEnabled).HasColumnName("Bloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.AccessFailedCount).HasColumnName("IngresosFallidos");

            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.UserName).HasColumnName("NombreUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.Email).HasColumnName("Email");
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}