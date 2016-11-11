using Domain.Entities;

namespace Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domain.MhgHrDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Domain.MhgHrDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.VacationsType.AddOrUpdate(p=>p.VacationName,
                new VacationsType { VacationName = "Sick leave" },
                new VacationsType { VacationName = "Occasional vacation" },
                new VacationsType { VacationName = "Unusual vacation" },
                new VacationsType { VacationName = "Maternity Leave" },
                new VacationsType { VacationName = "Haj Vacation" }

                );
            
            context.Brands.AddOrUpdate(
                p => p.BrandName,
                new Brand()
                {
                    BrandName = "Sera Macaw",

                },
                  new Brand()
                  {
                      BrandName = "CR7",

                  });


        }
    }
}
