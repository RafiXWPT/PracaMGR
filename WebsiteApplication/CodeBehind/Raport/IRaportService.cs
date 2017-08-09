namespace WebsiteApplication.CodeBehind.Raport
{
    public interface IRaportService
    {
        byte[] GenerateRaport(string patientPesel);
    }
}