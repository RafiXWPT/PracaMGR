using System.Linq;
using Domain.Residence;

namespace Domain.Interfaces
{
    public interface IHospitalizationRepository
    {
        IQueryable<Hospitalization> Hospitalizations { get; }
        void Update(Hospitalization hospitalization);
        void Add(Hospitalization hospitalization);
        void SaveChanges();
    }
}