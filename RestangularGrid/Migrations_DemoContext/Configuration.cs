namespace RestangularGrid.Migrations_DemoContext
{
    using FizzWare.NBuilder;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestangularGrid.Models.DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations_DemoContext";
        }

        protected override void Seed(RestangularGrid.Models.DemoContext context)
        {
            // NBuilder will create 200 test objects for us but we want Faker to fill in the actual data. 
            var customers = Builder<Customer>.CreateListOfSize(200)
                .All()
                    .With(c => c.FirstName = Faker.Name.First())
                    .With(c => c.LastName = Faker.Name.Last())
                .Build();

            foreach (var customer in customers)
            {
                context.Customers.AddOrUpdate(c => c.Id, customer);
            }
        }
    }
}
