using System.Linq;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        IQueryable<Address> Addresses { get; }
        void Update(Address address);
        void Add(Address address);
        void SaveChanges();
    }
}