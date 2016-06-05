using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public bool IsActive { get; set; }
        public virtual Employee Employee { get; set; }
        
    }
    public class MhgHrDataContext : IdentityDbContext<ApplicationUser>
    {
        public MhgHrDataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           
           
        }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Departments> Departments { get; set; }
        protected override void OnModelCreating(DbModelBuilder builder)
        {

            // Add other non-cascading FK declarations here
            builder.Entity<ApplicationUser>().HasOptional(u => u.Employee)
                .WithRequired(s => s.User).Map(a => a.MapKey("UserId"));
            Database.SetInitializer<MhgHrDataContext>(null);
            base.OnModelCreating(builder);
        }
        public static MhgHrDataContext Create()
        {
            return new MhgHrDataContext();
        }
    }
}
