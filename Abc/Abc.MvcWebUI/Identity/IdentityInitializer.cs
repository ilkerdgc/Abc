using Abc.MvcWebUI.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Identity
{
    public class IdentityInitializer: CreateDatabaseIfNotExists<IdentityDataContext> // CreateDatabaseIfNotExists database yok ise oluşturulsun diğer durumlarda oluşturulmasın demektir
    {
        protected override void Seed(IdentityDataContext context)
        {
            // rolleri oluşturalım
            if (!context.Roles.Any(x => x.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "admin",
                    Description = "admin rolü",
                };
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "user",
                    Description = "user rolü"
                };
                manager.Create(role);
            }

            // userları oluşturalım

            if (!context.Users.Any(x => x.Name == "ilkerdgc"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "ilker",
                    Surname = "dağcı",
                    UserName = "ilkerdgc",
                    Email = "ilker@gmail.com"
                };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }

            if (!context.Users.Any(x => x.Name == "ilkaydgc"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "ilkay",
                    Surname = "dağcı",
                    UserName = "ilkaydgc",
                    Email = "ilkay@gmail.com"
                };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");
            } 

            base.Seed(context);
        }
    }
}