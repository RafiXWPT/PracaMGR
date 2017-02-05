using System.Linq;
using Domain.Inventory;

namespace Domain.Interfaces
{
    public interface IMedicineRepository
    {
        IQueryable<Medicine> Medicines { get; }
        void Update(Medicine medicine);
        void Add(Medicine medicine);
        void SaveChanges();
    }
}