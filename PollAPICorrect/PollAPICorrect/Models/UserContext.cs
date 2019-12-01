using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollUser> PollUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<PollUser>().ToTable("PollUser");
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Vote>().ToTable("Vote");
            modelBuilder.Entity<Friendship>().ToTable("Friendship");
            
        }
    }
}
