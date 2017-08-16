using Domain;

namespace WebsiteApplication.CodeBehind.Raport
{
    public interface IRaportService
    {
        byte[] GenerateRaportBytes(string patientPesel);
        GeneratedReaport GenerateRaport(string patientPesel);
    }
}