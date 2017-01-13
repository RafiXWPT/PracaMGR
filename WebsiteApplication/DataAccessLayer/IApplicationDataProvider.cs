using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteApplication.DataAccessLayer
{
    interface IApplicationDataProvider
    {
        WebsiteDatabaseContext Database { get; }
    }
}
