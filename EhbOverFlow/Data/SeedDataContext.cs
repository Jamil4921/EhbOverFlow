using EhbOverFlow.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EhbOverFlow.Data
{
    public class SeedDataContext
    {
        public static async Task<IActionResult> Initialize(System.IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {

            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();   
                context.Database.Migrate();

                if(!context.Roles.Any())
                {
                    ApplicationUser ehbUser = new ApplicationUser
                    {
                        Email = "Ehbuser@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        UserName = "Ehbuser",
                        FirstName = "Sammy",
                        LastName = "Smith"
                        
                    };
                    ApplicationUser admin = new ApplicationUser
                    {
                        Email = "admin@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "Administrator",
                        FirstName = "Administrator",
                        LastName = "Administrator"

                    };

                    await userManager.CreateAsync(admin, "Abc!12345");
                    await userManager.CreateAsync(ehbUser, "Abc!12345");

                    context.Roles.AddRange
                        (
                            new IdentityRole { Id = "UserAdministrator", Name = "UserAdministrator", NormalizedName = "USERADMINISTRATOR" },
                            new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
                        );
                    context.SaveChanges();

                    string id = admin.Id;

                    context.UserRoles.AddRange
                        (
                            new IdentityUserRole<string> { RoleId = "User", UserId = ehbUser.Id },
                            new IdentityUserRole<string> { RoleId = "UserAdministrator", UserId = admin.Id }
                        );
                    context.SaveChanges();

                }
                ApplicationUser dummyUser = context.Users.FirstOrDefault(u => u.UserName == "Ehbuser");
                ApplicationUser administrator = context.Users.FirstOrDefault(u => u.UserName == "Administrator");

                return null;
            }

        }
    }
}
