using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    public enum SearchType
    {
        ALL_PATIENTS_FROM_INSTITUTION,
        PERSON_INFO,
        BASIC_HISTORY,
        FULL_HISTORY,
        HOSPITALIZATION,
        EXAMINATION,
        TREATMENT
    }

    public class SearchHistory
    {
        public Guid SearchHistoryId { get; set; }
        public string PatientPesel { get; set; }
        public Guid? HospitalizationId { get; set; }
        public Guid? ExaminationId { get; set; }
        public Guid? TreatmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public SearchType SearchType { get; set; }
    }
}
