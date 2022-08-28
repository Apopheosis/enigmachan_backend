using System;
using Microsoft.EntityFrameworkCore;

namespace Enigmachan.Models {
    
public class ThreadContext : DbContext
    {
        public ThreadContext (DbContextOptions<ThreadContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thread>()
                .HasIndex(s => new { s.post_id }).IsUnique();
            modelBuilder.Entity<Reply>()
                .HasIndex(s => new {s.post_id}).IsUnique();
            modelBuilder.Entity<Post>()
                .HasIndex(s => new {s.post_id}).IsUnique();
        }
    }
}