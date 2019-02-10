using IndividualNominationCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualNominationCatalogAPI
{
    public class IndividualNominationCatalogContext:DbContext
    {
        //constructor
        //DB injection -done for loose coupling so as not to tied to one type of db or a specific db instance
        //this allows us to choose any db (sql, mongo, postgrepsql) , or even replace db incase current one fails
        //the db connection string  is injected thru options
        //the db connection string is defined in 
        //this constructor will be called in Startup inroder to create the db
        public IndividualNominationCatalogContext(DbContextOptions options):base(options)
        {
        }

        //tables to be created
        public DbSet<AwardCategory> AwardCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SubOrg> SubOrgs { get; set; }
        public DbSet<Nomination> Nominations { get; set; }


        //overiding allows us to  define tables and its configuration, instead of generic tables
        //the call to this method will create the tables
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AwardCategory>(ConfigureAwardCategory);

            builder.Entity<Location>(ConfigureLocation);

            builder.Entity<SubOrg>(ConfigureSubOrg);

            builder.Entity<Nomination>(ConfigureNomination);

        }


        //define AwardCategory table schema
        private void ConfigureAwardCategory

           (EntityTypeBuilder<AwardCategory> builder)

        {

            builder.ToTable("AwardCategory");

            builder.Property(a => a.Id)

                .ForSqlServerUseSequenceHiLo("awardcategory_hilo")

                .IsRequired();

            builder.Property(a => a.Name)

                .IsRequired()

                .HasMaxLength(100);
            builder.Property(n => n.CreatedBy)

           .IsRequired()
           .HasMaxLength(30);

            builder.Property(n => n.CreatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();

            builder.Property(n => n.UpdatedBy)
                    .HasMaxLength(30);

            builder.Property(n => n.UpdatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAddOrUpdate();

        }



        //define Location table schema
        private void ConfigureLocation

           (EntityTypeBuilder<Location> builder)

        {

            builder.ToTable("Location");

            builder.Property(l => l.Id)

                .ForSqlServerUseSequenceHiLo("location_hilo")

                .IsRequired();

            builder.Property(l => l.Name)

                .IsRequired()

                .HasMaxLength(100);
            builder.Property(n => n.CreatedBy)

           .IsRequired()
           .HasMaxLength(30);

            builder.Property(n => n.CreatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();

            builder.Property(n => n.UpdatedBy)
          .HasMaxLength(30);

            builder.Property(n => n.UpdatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAddOrUpdate();

        }

        //define SubOrg table schema
        private void ConfigureSubOrg

           (EntityTypeBuilder<SubOrg> builder)

        {

            builder.ToTable("SubOrg");

            builder.Property(s => s.Id)

                .ForSqlServerUseSequenceHiLo("suborg_hilo")

                .IsRequired();

            builder.Property(s => s.Name)

                .IsRequired()

                .HasMaxLength(100);

            builder.Property(n => n.CreatedBy)

           .IsRequired()
           .HasMaxLength(30);

            builder.Property(n => n.CreatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();

            builder.Property(n => n.UpdatedBy)
          .HasMaxLength(30);

            builder.Property(n => n.UpdatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAddOrUpdate();

        }


        //define Nomination table schema
        private void ConfigureNomination

           (EntityTypeBuilder<Nomination> builder)

        {

            builder.ToTable("Nomination");

            builder.Property(n => n.Id)

                .ForSqlServerUseSequenceHiLo("nomination_hilo")

                .IsRequired();

            builder.Property(n => n.Alias)

                .IsRequired()

                .HasMaxLength(30);

            builder.Property(n => n.Headline)

                .IsRequired()

                .HasMaxLength(100);

            builder.Property(n => n.DescriptionComments)

               .IsRequired()

               .HasMaxLength(255);

            builder.Property(n => n.ImpactComments)

               .IsRequired()

               .HasMaxLength(255);
            builder.Property(n => n.ReviewStatus)

               .IsRequired();



            builder.Property(n => n.Winner)

            .IsRequired();
            
            builder.Property(n => n.CreatedBy)

            .IsRequired()
            .HasMaxLength(30);

            builder.Property(n => n.CreatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();

            builder.Property(n => n.UpdatedBy)
                .HasMaxLength(30);

            builder.Property(n => n.UpdatedDate)

                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(n => n.AwardsCategory)

                .WithMany()

                .HasForeignKey(n => n.AwardCategoryId);
            builder.HasOne(n => n.Location)

                .WithMany()

                .HasForeignKey(n => n.LocationId);

            builder.HasOne(n => n.SubOrg)

               .WithMany()

               .HasForeignKey(n => n.SubOrgId);


        }
    }
}
