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

            //string EntryDll = System.Reflection.Assembly.GetEntryAssembly().Location;
            //EntryDll        = System.IO.Path.GetFileName( EntryDll ).ToLower();

            //if ( EntryDll == "ef.dll" ) {
            //    Console.Out.WriteLine( "\nDatabase Update Detected\n" );
            //    this.Database.EnsureDeleted();
            //}

            this.Database.EnsureCreated();
        }

        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<Couple> Couples { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            modelBuilder.Entity<Dinner>( table => {

                table.HasOne( o => o.Admin )
                  .WithOne()
                  .HasForeignKey<Dinner>( o => o.AdminID )
                  .OnDelete( DeleteBehavior.Restrict );

                table.HasMany( o => o.Couples )
                  .WithOne( o => o.Dinner );
            });


            modelBuilder.Entity<Couple>( table => {

                table.HasOne( o => o.Address )
                      .WithOne()
                      .HasForeignKey<Couple>( o => o.AddressID );

                table.HasOne( o => o.Dinner )
                      .WithMany( o => o.Couples )
                      .HasForeignKey( o => o.DinnerID );

                table.HasOne( o => o.PersonMain )
                      .WithOne()
                      .HasForeignKey<Couple>( o => o.PersonMainID )
                      .OnDelete( DeleteBehavior.Restrict );

                table.HasOne( o => o.PersonGuest )
                      .WithOne()
                      .HasForeignKey<Couple>( o => o.PersonGuestID )
                      .OnDelete( DeleteBehavior.Restrict );
            } );

            base.OnModelCreating( modelBuilder );
        }
    }
}
