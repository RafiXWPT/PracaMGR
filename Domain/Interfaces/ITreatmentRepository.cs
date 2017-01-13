using System.Linq;
using Domain.Residence;

namespace Domain.Interfaces
{
    public interface ITreatmentRepository
    {
        IQueryable<Treatment> Treatments { get; }
        void Update(Treatment treatment);
        void Add(Treatment treatment);
        void SaveChanges();
    }
}