using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialBookmarking_FM.Models;

namespace SocialBookmarking_FM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Vote> Votes { get; set; }

        public DbSet<Collection> Collection { get; set; }

        public DbSet<CollectionCategory> CollectionCategory { get; set; }

        public DbSet<BookmarkCollection> BookmarkCollection { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>()
                  .HasKey(m => new { m.UserId, m.BookmarkId });

            modelBuilder.Entity<BookmarkCollection>()
                .HasIndex(p => new { p.BookmarkId, p.CollectionId }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}