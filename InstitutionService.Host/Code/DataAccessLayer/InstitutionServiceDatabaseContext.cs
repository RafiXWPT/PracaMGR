using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using Domain.Residence;

namespace InstitutionService.Host.Code.DataAccessLayer
{
    public class InstitutionServiceDatabaseContext : DbContext, IRepository
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<UsedMedicine> UsedMedicines { get; set; }
        
        static InstitutionServiceDatabaseContext()
        {
            Database.SetInitializer<InstitutionServiceDatabaseContext>(null);           
        }

        public InstitutionServiceDatabaseContext() : base(ConfigurationProvider.Instance.GetValue("INSTITUTION_DATABASE_NAME")) { }

        public InstitutionServiceDatabaseContext(string connectionName) : base(connectionName) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
