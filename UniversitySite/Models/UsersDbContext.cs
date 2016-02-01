using System.Data.Entity;
using Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniversitySite.Migrations;

namespace UniversitySite.Models
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public UsersDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UsersDbContext, Configuration>());
        }

        public static UsersDbContext Create()
        {
            return new UsersDbContext();
        }
    }
}