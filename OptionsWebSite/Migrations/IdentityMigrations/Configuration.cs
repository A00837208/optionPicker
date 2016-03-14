namespace OptionsWebSite.Migrations.IdentityMigrations
{
    using DiplomaDataModel.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Identity.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
            ContextKey = "DiplomaDataModel.Identity.ApplicationDbContext";
        }

        protected override void Seed(DiplomaDataModel.Identity.ApplicationDbContext context)
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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("Student"))
                roleManager.Create(new IdentityRole("Student"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            string[] emails = { "a@a.a", "s@s.s" };
            string[] usernames = { "A00111111", "A00222222"};

            if (userManager.FindByName(usernames[0]) == null)
            {
                var user = new ApplicationUser
                {
                    Email = emails[0],
                    UserName = usernames[0],
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(user.UserName).Id, "Admin");
            }
            if (userManager.FindByName(usernames[1]) == null)
            {
                var user = new ApplicationUser
                {
                    Email = emails[1],
                    UserName = usernames[1],
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(user.UserName).Id, "Student");
            }
        }
    }
}
