using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;
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
                    ApplicationUser user = new ApplicationUser {

                        Email = "Timmy@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "Timmy",
                        FirstName = "Timmy",
                        LastName = "Smith"
                    };

                    await userManager.CreateAsync(admin, "Abc!12345");
                    await userManager.CreateAsync(ehbUser, "Abc!12345");
                    await userManager.CreateAsync(user, "Abc!12345");

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
                            new IdentityUserRole<string> { RoleId = "UserAdministrator", UserId = admin.Id },
                            new IdentityUserRole<string> { RoleId = "User", UserId = user.Id }
                        );
                    context.SaveChanges();

                }
                ApplicationUser ehbUsers = context.Users.FirstOrDefault(u => u.UserName == "Ehbuser");
                ApplicationUser Administrator = context.Users.FirstOrDefault(u => u.UserName == "Administrator");
                ApplicationUser user1 = context.Users.FirstOrDefault(u => u.UserName == "Timmy");


                if (!context.Categories.Any())
                {
                    context.Categories.AddRange
                        (

                            new Category { SubjectName = "Lecture notes"},
                            new Category { SubjectName = "Werkcolleges"},
                            new Category { SubjectName = "Solutions"},
                            new Category { SubjectName = "Questions"}
                        );
                    context.SaveChanges();
                }

               

                if (!context.notes.Any())
                {
                    var categories = context.Categories.ToList();
                    var lectureNotesCategory = categories.FirstOrDefault(c => c.SubjectName == "Lecture notes");
                    var werkCollegeNotesCategory = categories.FirstOrDefault(c => c.SubjectName == "Werkcolleges");
                    var SolutionCollegeNotesCategory = categories.FirstOrDefault(c => c.SubjectName == "Solutions");
                    var QuestionsCollegeNotesCategory = categories.FirstOrDefault(c => c.SubjectName == "Questions");

                    context.notes.AddRange
                        (

                            new Note
                            {
                                Title = "Why is my ForLoop not working??",
                                Body = "Im trying to userstand the concept of a for loop but i cant seem to make it work. what im i doing wrong?",
                                CreatedDate= DateTime.Now,
                                Solved = false,
                                Image = "Screenshot_20230130_024509.png",
                                UserName = "",
                                UserId = ehbUsers.Id,
                                CategoryId = lectureNotesCategory.Id
                            },

                            new Note
                            {
                                Title = "Where can i get my API KEy again for Android studio?",
                                Body = "Im trying to get the API key. but i dont know where to find one?",
                                CreatedDate= DateTime.Now,
                                Solved = true,
                                Image = "Screenshot_20230130_025826.png",
                                UserName = "",
                                UserId = ehbUsers.Id,
                                CategoryId = werkCollegeNotesCategory.Id
                            },

                            new Note
                            {
                                Title = "What is best way to learn programming?",
                                Body = "Im Really struggeling with proggraming an d i dont know where to start?",
                                CreatedDate= DateTime.Now,
                                Solved = true,
                                Image = "",
                                UserName = "",
                                UserId = user1.Id,
                                CategoryId = QuestionsCollegeNotesCategory.Id
                            },
                            new Note
                            {
                                Title = "Solution POPUP android ",
                                Body = "Solution for the excersize for oef 6 can be found in the modules section",
                                CreatedDate= DateTime.Now,
                                Solved = true,
                                Image = "Screenshot_20230130_031432.png",
                                UserName = "",
                                UserId = ehbUsers.Id,
                                CategoryId = SolutionCollegeNotesCategory.Id
                            }

                        ) ;
                    context.SaveChanges();
                }



                return null;
            }

        }
    }
}
