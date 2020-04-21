using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Database {

    public class DatabaseContext : DbContext {
        
        public DatabaseContext( [NotNull] DbContextOptions options ) : base( options ) {

            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<Couple> Couples { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            var dinner = modelBuilder.Entity<Dinner>();
            dinner.HasMany( o => o.Couples ).WithOne( o => o.Dinner);

            var couple = modelBuilder.Entity<Couple>();
            couple.HasOne( o => o.Address );
            couple.HasOne( o => o.PersonMain ).WithOne().OnDelete( DeleteBehavior.Restrict );
            couple.HasOne( o => o.PersonExtra ).WithOne().OnDelete( DeleteBehavior.Restrict );

            base.OnModelCreating( modelBuilder );
        }
    }
}
