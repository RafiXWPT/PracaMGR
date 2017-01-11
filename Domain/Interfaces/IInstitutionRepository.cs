using System.Linq;

namespace Domain.Interfaces
{
    public interface IInstitutionRepository
    {
        IQueryable<Institution> Institutions { get; }
        void Add();
        void Update();
        void SaveChanges();
    }
}