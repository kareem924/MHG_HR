using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Domain
{
   
    public class MhgHrDataContext : DbContext
    {
        public MhgHrDataContext()
            : base("DefaultConnection")
        {


        }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Departments> Departments { get; set; }
        public IDbSet<Branch> Branches { get; set; }
        public IDbSet<Brand> Brands { get; set; }
        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<Agenda> Agendas { get; set; }
        public IDbSet<DependencyAgenda> DependencyAgendas { get; set; }
        public IDbSet<Tasks> Tasks { get; set; }
        public IDbSet<UserDocuments> UsersDocument { get; set; }
        public IDbSet<OfficalHolidays> OfficalHolidays { get; set; }
        public IDbSet<Vacation> Vacations { get; set; }
        public IDbSet<VacationsType> VacationsType { get; set; }
        public IDbSet<Notifications> Notifications { get; set; }
        public IDbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //Branch brands  declarations
            builder.Entity<Branch>()
                 .HasRequired<Brand>(s => s.Brand)
                 .WithMany(s => s.branshes)
                 .HasForeignKey(s => s.BrandId);
            //Borrow BorrowDistribution  declarations
            builder.Entity<BorrowDistribution>()
                .HasRequired<Borrow>(s => s.Borrow)
                .WithMany(s => s.BorrowDistributions)
                .HasForeignKey(s => s.BorrowId);
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(builder);
        }
        public static MhgHrDataContext Create()
        {
            return new MhgHrDataContext();
        }
    }
}
