using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RoleToRight
    {
        public Guid RoleToRightId { get; set; }
        public string Role { get; set; }
        public Guid RightId { get; set; }
        public virtual Right Right { get; set; }
    }
}
