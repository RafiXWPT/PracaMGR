using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;

namespace PersonInfoService.Host.Code.DataAccessLayer
{
    public class PersonInfoServiceDatabaseContext : DbContext
    {
        static PersonInfoServiceDatabaseContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PersonInfoServiceDatabaseContext>());
        }

        public PersonInfoServiceDatabaseContext() : base("BazaOsob")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}