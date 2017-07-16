using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace WebsiteApplication.CodeBehind
{
    public interface IRaportService
    {
        byte[] GenerateRaport(string patientPesel);
    }
}