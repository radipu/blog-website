﻿using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Areas.Public.Models;
using My_Blog_Website.Models;

namespace My_Blog_Website.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Cascade); // Auto-deletes comments when post is deleted
            
            modelBuilder.Entity<ReactionVote>()
                .HasOne(r => r.Comment)
                .WithMany(p => p.ReactionVotes)
                .OnDelete(DeleteBehavior.Cascade); // Auto-deletes reactions
        }

        public DbSet<Authors> authors { get; set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Posts> posts { get; set; }
        public DbSet<PostReactionVote> postReactionVotes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReactionVote> ReactionVotes { get; set; }
        public DbSet<Subscriber> subscribers { get; set; }
        public DbSet<FAQs> faqs { get; set; }
    }
}
