using EhbOverFlow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;


namespace EhbOverFlow.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Note> notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<MainComment> mainComments { get; set; }
        public DbSet<SubComment> subComments { get; set; }

        public DbSet<Likes> likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(n => n.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
            .HasMany(c => c.CatNotes)
            .WithOne(n => n.Category)
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}