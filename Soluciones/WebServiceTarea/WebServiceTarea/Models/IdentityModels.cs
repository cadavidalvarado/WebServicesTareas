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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.UserName).HasColumnName("EmailUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.Email).HasColumnName("Correo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.EmailConfirmed).HasColumnName("EmailConfirmacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.PasswordHash).HasColumnName("Contrasena");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.SecurityStamp).HasColumnName("Seguridad");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.PhoneNumber).HasColumnName("Telefono");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.TwoFactorEnabled).HasColumnName("FactoresActivacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.LockoutEndDateUtc).HasColumnName("FechaBloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.LockoutEnabled).HasColumnName("Bloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuarios").Property(p => p.AccessFailedCount).HasColumnName("IngresosFallidos");

            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.UserName).HasColumnName("EmailUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.Email).HasColumnName("Correo");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.EmailConfirmed).HasColumnName("EmailConfirmacion");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.PasswordHash).HasColumnName("Contrasena");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.SecurityStamp).HasColumnName("Seguridad");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.PhoneNumber).HasColumnName("Telefono");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmacion");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.TwoFactorEnabled).HasColumnName("FactoresActivacion");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.LockoutEndDateUtc).HasColumnName("FechaBloqueo");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.LockoutEnabled).HasColumnName("Bloqueo");
            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios").Property(p => p.AccessFailedCount).HasColumnName("IngresosFallidos");

            modelBuilder.Entity<IdentityUserRole>().ToTable("UsuarioRol").Property(p => p.RoleId).HasColumnName("IdRol");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UsuarioRol").Property(p => p.UserId).HasColumnName("IdUsuario");

            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuarioAutenticacion").Property(p => p.LoginProvider).HasColumnName("IdLogin");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuarioAutenticacion").Property(p => p.ProviderKey).HasColumnName("IdLlaveProveedor");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuarioAutenticacion").Property(p => p.UserId).HasColumnName("IdUsuario");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioReclamo").Property(p => p.Id).HasColumnName("IdUsuarioReclamo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioReclamo").Property(p => p.UserId).HasColumnName("IdUsuario");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioReclamo").Property(p => p.ClaimType).HasColumnName("TipoReclamo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioReclamo").Property(p => p.ClaimValue).HasColumnName("Reclamo");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(p => p.Id).HasColumnName("IdRol");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(p => p.Name).HasColumnName("NombreRol");

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