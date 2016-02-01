using System.Data.Entity.Migrations;
using UniversitySite.Models;

namespace UniversitySite.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UsersDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UsersDbContext context)
        {
        }
    }
}