using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.userDocuemts = new HashSet<UserDocuments>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public bool IsActive { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<UserDocuments> userDocuemts { get; set; }

    }
    public class MhgHrDataContext : IdentityDbContext<ApplicationUser>
    {
        public MhgHrDataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {


        }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Departments> Departments { get; set; }
        public IDbSet<Branch> Branches { get; set; }
        public IDbSet<Brand> Brands { get; set; }
        public IDbSet<Job> jobs { get; set; }
        public IDbSet<UserDocuments> UsersDocument { get; set; }
        public IDbSet<OfficalHolidays> OfficalHolidays { get; set; }
        protected override void OnModelCreating(DbModelBuilder builder)
        {

            // Add other non-cascading FK declarations here
            builder.Entity<ApplicationUser>().HasOptional(u => u.Employee)
                .WithRequired(s => s.User).Map(a => a.MapKey("UserId"));

            builder.Entity<UserDocuments>()
                  .HasRequired<ApplicationUser>(s => s.user)
                  .WithMany(s => s.userDocuemts)
                  .HasForeignKey(s => s.UserId);
            builder.Entity<Branch>()
                 .HasRequired<Brand>(s => s.Brand)
                 .WithMany(s => s.branshes)
                 .HasForeignKey(s => s.BrandId);
            builder.Entity<BorrowDistribution>()
                .HasRequired<Borrow>(s => s.Borrow)
                .WithMany(s => s.BorrowDistributions)
                .HasForeignKey(s => s.BorrowId);
            Database.SetInitializer<MhgHrDataContext>(null);
            base.OnModelCreating(builder);
        }
        public static MhgHrDataContext Create()
        {
            return new MhgHrDataContext();
        }
    }
}
