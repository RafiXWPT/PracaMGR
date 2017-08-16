using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum SearchType
    {
        PERSON_INFO,
        BASIC_HISTORY,
        FULL_HISTORY,
        HOSPITALIZATION,
        EXAMINATION,
        TREATMENT
    }

    public class SearchHistory
    {
        public Guid Id { get; set; }
        public string PatientPesel { get; set; }
        public DateTime SearchedAt { get; set; }
        public string SearchedBy { get; set; }
        public SearchType SearchType { get; set; }
    }
}
