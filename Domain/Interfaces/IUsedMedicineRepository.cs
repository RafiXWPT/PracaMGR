using System.Linq;
using Domain.Inventory;

namespace Domain.Interfaces
{
    public interface IUsedMedicineRepository
    {
        IQueryable<UsedMedicine> UsedMedicines { get; }
        void Update(UsedMedicine usedMedicine);
        void Add(UsedMedicine usedMedicine);
        void SaveChanges();
    }
}