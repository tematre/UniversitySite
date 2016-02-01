
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Identity;
using Domain.University;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniversitySite;
using UniversitySite.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CreateRoles), CreateRoles.StartMethodName)]
namespace UniversitySite
{
    public class CreateRoles
    {
        public const string StartMethodName = "Start";

        private static readonly List<string> RolesList = new List<string> { Constants.Roles.Admin, Constants.Roles.Student, Constants.Roles.Professor };


        public static void Start()
        {
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new UsersDbContext())))
            {
                foreach (var roleName in RolesList.Where(roleName => !roleManager.RoleExists(roleName)))
                {
                    roleManager.Create(new IdentityRole(roleName));
                }

            }
            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UsersDbContext())))
            {
                if (userManager.FindByName(Constants.AdminUserName) != null)
                {
                    return;
                }

                var admin = new ApplicationUser { UserName = Constants.AdminUserName };
                var result = userManager.Create(admin, "AdminPass");
                if (!result.Succeeded)
                {
                    var txt = new StringBuilder();

                    foreach (var error in result.Errors)
                    {
                        txt.AppendLine(error);
                    }
                    throw new Exception(txt.ToString());
                }

                userManager.AddToRole(admin.Id, Constants.Roles.Admin);
            }
        }
    }
}