using System.Linq;
using Domain.Residence;

namespace Domain.Interfaces
{
    public interface IExaminationRepository
    {
        IQueryable<Examination> Examinations { get; }
        void Update(Examination examination);
        void Add(Examination examination);
        void SaveChanges();
    }
}