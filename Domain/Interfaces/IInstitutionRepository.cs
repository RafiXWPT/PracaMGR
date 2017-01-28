using System.Linq;

namespace Domain.Interfaces
{
    public interface IInstitutionRepository
    {
        IQueryable<Institution> Institutions { get; }
        void Add(Institution institution);
        void Update(Institution institution);
        void Delete(Institution institution);
        void SaveChanges();
    }
}