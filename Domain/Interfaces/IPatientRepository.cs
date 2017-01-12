using System.Linq;

namespace Domain.Interfaces
{
    public interface IPatientRepository
    {
        IQueryable<Patient> Patients { get; }
        void Update(Patient patient);
        void Add();
        void SaveChanges();
    }
}