using System;
using System.Collections.Generic;
using System.Linq;
using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics.Races;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ItemHoarderMock.API.Startup))]

namespace ItemHoarderMock.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            //MakeRaces();
            MakeRoles();
        }
        //public void MakeRaces()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    DnDRace race = new DnDRace();
        //    race.WeaponProficiencies = "AHH";
        //    PathfinderRace race2 = new PathfinderRace();
        //    race2.BaseSpeed = 7;
        //    context.Races.Add(race);
        //    context.Races.Add(race2);
        //    context.SaveChanges();
        //}
        private void MakeRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //creating admin role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var adminUser = new ApplicationUser();
                adminUser.UserName = "admin";
                adminUser.Email = "admin@admin.com";
                string adminPWD = "Admin1!";
                var chkUser = UserManager.Create(adminUser, adminPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(adminUser.Id, "Admin");
                }
            }
            //creating GM role
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
