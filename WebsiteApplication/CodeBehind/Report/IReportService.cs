using Domain;

namespace WebsiteApplication.CodeBehind.Report
{
    public interface IReportService
    {
        byte[] GenerateRaportBytes(string patientPesel, string username);
        GeneratedReport GenerateReport(string patientPesel, string username);
    }
}