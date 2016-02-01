using Domain.Identity;

namespace UniversitySite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversitySite.Models.UsersDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UniversitySite.Models.UsersDbContext context)
        {
           
        }
    }
}
