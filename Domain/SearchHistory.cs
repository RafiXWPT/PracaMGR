using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SearchHistory
    {
        public Guid Id { get; set; }
        public string PatientPesel { get; set; }
        public DateTime SearchedAt { get; set; }
    }
}
