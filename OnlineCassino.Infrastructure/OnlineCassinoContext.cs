using OnlineCassino.Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OnlineCassino.Infrastructure
{
    public class OnlineCassinoContext : DbContext
    {
        public OnlineCassinoContext() : base("OnlineCassinoContext")
        {

        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameCollection> GameCollections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Add(new DateTime2Convention());

            modelBuilder.Entity<Game>().Property(c => c.ReleaseDate).HasColumnType("datetime2");
        }
    }

    //public class DateTime2Convention : Convention
    //{
    //    public DateTime2Convention()
    //    {
    //        this.Properties<DateTime>()
    //            .Configure(c => c.HasColumnType("datetime2"));
    //    }
    //}
}