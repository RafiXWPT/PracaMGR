using Domain;

namespace WebsiteApplication.CodeBehind.Raport
{
    public interface IRaportService
    {
        byte[] GenerateRaportBytes(string patientPesel, string username);
        GeneratedReport GenerateRaport(string patientPesel, string username);
    }
}