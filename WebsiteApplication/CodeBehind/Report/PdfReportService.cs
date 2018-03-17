using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using WebsiteApplication.CodeBehind.WcfServices;

namespace WebsiteApplication.CodeBehind.Report
{
    public class PdfReportService : IReportService
    {
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IRepository<Institution> _institutionRepository;
        private readonly IRepository<SearchHistory> _searchHistoryRepository;

        public PdfReportService(IRepository<Institution> institutionRepository, IRepository<SearchHistory> searchHistoryRepository)
        {
            _institutionRepository = institutionRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _personInfoFetcher = new WcfPersonInfoFetcher();
        }

        public byte[] GenerateRaportBytes(string patientPesel, string username)
        {
            var personInfo = _personInfoFetcher.GetPersonInfo(patientPesel);
            if (personInfo == null)
                return null;

            var patient = new WcfDataFetcher(_institutionRepository, _searchHistoryRepository, username).GetPatient<PatientTransferObject>(patientPesel, true);

            return GeneratePdf(personInfo, patient);
        }

        public GeneratedReport GenerateReport(string patientPesel, string username)
        {
            var generatedPdfBytes = GenerateRaportBytes(patientPesel, username);
            return new GeneratedReport
            {
                PatientPesel = patientPesel,
                CreatedAt = DateTime.Now,
                Report = generatedPdfBytes
            };
        }

        private byte[] GeneratePdf(PersonTransferObject personInfo, PatientTransferObject patient)
        {
            var ms = new MemoryStream();
            var pdfWriter = new PdfWriter(ms);
            var pdf = new PdfDocument(pdfWriter);
            var document = new Document(pdf, PageSize.A4);
            document.SetFont(PdfFontFactory.CreateFont("C:/Windows/fonts/arial.ttf", "CP1250", true));

            document.Add(new Paragraph("System informacji medycznej").SetFontSize(25)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER));
            GeneratePersonInfo(document, personInfo);
            GenerateBasicHospitalizationInfo(document, patient);
            GenerateDetailedHospitalizationInfo(document, patient);
            document.Close();

            return ms.ToArray();
        }

        private void GeneratePersonInfo(Document document, PersonTransferObject personInfo)
        {
            document.Add(new Paragraph("Raport dotyczy osoby:").SetMarginTop(20).SetFontSize(20));
            var personInfoParagraph = new Paragraph();
            personInfoParagraph.Add($"PESEL: {personInfo.Pesel}");
            personInfoParagraph.Add($"\nImię: {personInfo.FirstName}");
            personInfoParagraph.Add($"\nNazwisko: {personInfo.LastName}");
            personInfoParagraph.Add($"\nData urodzenia: {personInfo.BirthDate}");
            personInfoParagraph.Add($"\nNumer ubezpieczenia: {personInfo.InsuranceId}");

            personInfoParagraph.Add("");
            personInfoParagraph.Add(
                $"\nAdres: {personInfo.Address.City} ul. {personInfo.Address.Street} {personInfo.Address.HomeNumber}");

            document.Add(personInfoParagraph);
        }

        private void GenerateBasicHospitalizationInfo(Document document, PatientTransferObject patient)
        {
            document.Add(new Paragraph("Dane ogólne na temat hospitalizacji:").SetFontSize(20));

            var hasBeenTreatmentInInstitution = patient.Hospitalizations.Any(x => x.Treatments.Any());
            var hasBeenExaminedInInstitution = patient.Hospitalizations.Any(x => x.Examinations.Any());
            var numberOfInstitutions = patient.Hospitalizations.GroupBy(h => h.InstitutionId)
                .Select(g => g.Key)
                .Count();

            var infoParagraph = new Paragraph();
            infoParagraph.Add(
                $"Ilość placówek w których osoba była leczona: {numberOfInstitutions}");
            infoParagraph.Add(
                $"\nIlość wizyt w placówkach ogółem: {patient.Hospitalizations.Count}");
            infoParagraph.Add(
                $"\nPierwsze wizyta w placówce medycznej: {patient.Hospitalizations.Min(x => x.HospitalizationStartTime)}");
            infoParagraph.Add(
                $"\nOstatnia wizyta w placówce medycznej: {patient.Hospitalizations.Max(x => x.HospitalizationEndTime)}");
            infoParagraph.Add(
                $"\n{(hasBeenExaminedInInstitution ? "Osoba była badana" : "Osoba nigdy nie była badana")}");
            if (hasBeenExaminedInInstitution)
                infoParagraph.Add(
                    $"\nCałkowita ilość przeprowadzonych badań: {patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count)}");
            infoParagraph.Add(
                $"\n{(hasBeenTreatmentInInstitution ? "Osoba była operowana" : "Osoba nigdy nie była operowana")}");
            if (hasBeenTreatmentInInstitution)
                infoParagraph.Add(
                    $"\nCałkowita ilość przeprowadzonych operacji: {patient.Hospitalizations.Sum(hospitalization => hospitalization.Treatments.Count)}");
            document.Add(infoParagraph);
        }

        private void GenerateDetailedHospitalizationInfo(Document document, PatientTransferObject patient)
        {
            var hospitalizations = patient.Hospitalizations.OrderBy(x => x.HospitalizationStartTime);
            var institutionGroups = hospitalizations.GroupBy(x => x.InstitutionId).ToList();
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph("Dane szczegółowe na temat hospitalizacji:").SetFontSize(20));
            foreach (var institutionKey in institutionGroups)
            foreach (var hospitalization in institutionKey.ToList())
            {
                GenerateNewHospitalizationInfo(document, hospitalization);
                if(institutionKey == institutionGroups.Last() && hospitalization.HospitalizationId == institutionKey.Last().HospitalizationId)
                        continue;

                document.GetPdfDocument().AddNewPage();
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            }

        }

        private void GenerateNewHospitalizationInfo(Document document,
            HospitalizationTransferObject hospitalization)
        {
            var currentInstitutionName = _institutionRepository.Read(hospitalization.InstitutionId).InstitutionName;
            document.Add(new Paragraph($"{currentInstitutionName}:").SetFontSize(20));

            var datesList = new List();
            datesList.Add(new ListItem($"Początek hospitalizacji: {hospitalization.HospitalizationStartTime}"));
            datesList.Add(new ListItem($"Koniec hospitalizacji: {hospitalization.HospitalizationEndTime}"));
            document.Add(datesList);

            GenerateExaminationTable(document, hospitalization.Examinations);
            GenerateTreatmentTable(document, hospitalization.Treatments);
        }

        private void GenerateExaminationTable(Document document, List<ExaminationTransferObject> examinations)
        {
            document.GetPdfDocument().AddNewPage();
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph("Przeprowadzone badania").SetFontSize(20));

            var examinationTable = GetExaminationTable();
            foreach (var examination in examinations.OrderBy(x => x.ExaminationStartTime))
            {
                examinationTable.AddCell($"{examination.ExaminationStartTime}");
                examinationTable.AddCell($"{examination.ExaminationEndTime}");
                examinationTable.AddCell($"{examination.ExaminationDetails}");
            }
            examinationTable.AddCell($"Całkowita ilość badań: {examinations.Count}");

            document.Add(examinationTable);
        }

        private void GenerateTreatmentTable(Document document, List<TreatmentTransferObject> treatments)
        {
            document.GetPdfDocument().AddNewPage();
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph("Przeprowadzone operacje:").SetFontSize(20));

            var treatmentTable = GetTreatmentTable();
            foreach (var treatment in treatments.OrderBy(x => x.TreatmentDateTime))
            {
                treatmentTable.AddCell($"{treatment.TreatmentDateTime}");
                var medicineList = new List().SetListSymbol("");
                foreach (var medicine in treatment.UsedMedicines)
                    medicineList.Add(new ListItem($"{medicine.Medicine.MedicineName} - {medicine.Dose:F2} mg"));
                treatmentTable.AddCell(medicineList);
            }
            treatmentTable.AddCell($"Całkowita ilość operacji: {treatments.Count}");

            document.Add(treatmentTable);
        }

        private Table GetExaminationTable()
        {
            var table = new Table(new float[] {4, 4, 4}).SetWidthPercent(100).SetKeepTogether(true);
            table.AddHeaderCell("Początek badania");
            table.AddHeaderCell("Zakończenie badania");
            table.AddHeaderCell("Szczegóły badania");
            return table;
        }

        private Table GetTreatmentTable()
        {
            var table = new Table(new float[] {4, 4}).SetWidthPercent(100).SetKeepTogether(true);
            table.AddHeaderCell("Data operacji");
            table.AddHeaderCell("Użyte medykamenty");
            return table;
        }
    }
}