using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;

namespace PersonInfoService.Host.Code.DataAccessLayer
{
    public class PersonInfoServiceDatabaseContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }

        static PersonInfoServiceDatabaseContext()
        {
            Database.SetInitializer<PersonInfoServiceDatabaseContext>(null);
        }

        public PersonInfoServiceDatabaseContext() : base("PersonInfoContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
