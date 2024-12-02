using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class TournamentAPIContext : DbContext
    {
        public TournamentAPIContext (DbContextOptions<TournamentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentDetails> TournamentDetails { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TournamentDetails>().HasMany(t => t.Games)
                                                    .WithOne(g => g.Tournament)
                                                    .HasForeignKey(t => t.TournamentId)
                                                    .IsRequired()
                                                    .OnDelete(DeleteBehavior.Cascade);
                                                    
        }

    }
}
