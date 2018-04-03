using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    public class HospitalizationDocumentRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<HospitalizationDocument>
    {
        public HospitalizationDocumentRepository(IDbRepository context) : base(context)
        {
        }

        public IQueryable<HospitalizationDocument> Entities => Context.HospitalizationDocuments;
        public void Create(HospitalizationDocument entity)
        {
            throw new NotImplementedException();
        }

        public HospitalizationDocument Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.HospitalizationDocumentId == entityId);
        }

        public void Update(HospitalizationDocument entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(HospitalizationDocument entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
