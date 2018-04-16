using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using WebGrease.Css.Extensions;
using WebsiteApplication.CodeBehind.WcfServices;

namespace WebsiteApplication.CodeBehind.Report
{
    public class PdfReportService : IReportService
    {
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IRepository<Institution> _institutionRepository;
        private readonly IDateTimeCountableRepository<SearchHistory> _searchHistoryRepository;

        public PdfReportService(IRepository<Institution> institutionRepository, IDateTimeCountableRepository<SearchHistory> searchHistoryRepository)
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

            var hasBeenThreatedInInstitution = patient.Hospitalizations.Any(x => x.Treatments.Any());
            var hasBeenExaminedInInstitution = patient.Hospitalizations.Any(x => x.Examinations.Any());
            var hasBeenOnSickLeave = patient.Hospitalizations.Any(hospitalization => hospitalization.Examinations.Any(e => e.SickLeave));
            var numberOfInstitutions = patient.Hospitalizations.GroupBy(h => h.InstitutionId)
                .Select(g => g.Key)
                .Count();

            var globalInfoTable = new Table(4);
            globalInfoTable.AddCell(GenerateTextCell(1, 3, "Ilość placówek w których osoba była leczona:"));
            globalInfoTable.AddCell(GenerateTextCell(1, 1, $"{numberOfInstitutions}"));
            globalInfoTable.AddCell(GenerateTextCell(1, 3, "Ilość wizyt w placówkach ogółem:"));
            globalInfoTable.AddCell(GenerateTextCell(1, 1, $"{patient.Hospitalizations.Count}"));
            globalInfoTable.AddCell(GenerateTextCell(1, 2, "Pierwsza wizyta w placówce medycznej:"));
            globalInfoTable.AddCell(GenerateTextCell(1, 2, $"{patient.Hospitalizations.Min(x => x.HospitalizationStartTime)}"));
            globalInfoTable.AddCell(GenerateTextCell(1, 2, "Ostatnia wizyta w placówce medycznej:"));
            globalInfoTable.AddCell(GenerateTextCell(1, 2, $"{patient.Hospitalizations.Max(x => x.HospitalizationEndTime)}"));

            document.Add(globalInfoTable);
            document.Add(new Paragraph(""));
            document.Add(new LineSeparator(new SolidLine(2)));
            document.Add(new Paragraph(""));

            var globalInfoExaminationTable = new Table(4);
            globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Osoba była kiedykolwiek badana:").SetBackgroundColor(Color.GRAY));
            globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1, BoolToYesNo(hasBeenExaminedInInstitution)));
            if (hasBeenExaminedInInstitution)
            {
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Całkowita ilość przeprowadzonych badań:"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                    $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count)}"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Wizyty prywatne:"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                    $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count(e => e.PrivateVisit))}"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Wizyty publiczne:"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                    $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count(e => !e.PrivateVisit))}"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Przypisane recepty:"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                    $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count(e => e.SignedReceip))}"));
                globalInfoExaminationTable.AddCell(
                    GenerateTextCell(1, 3, "Osoba odbywała kiedykolwiek zwolnienie lekarskie:")
                        .SetBackgroundColor(Color.GRAY));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1, BoolToYesNo(hasBeenOnSickLeave)));
                if (hasBeenOnSickLeave)
                {
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3,
                        "Całkowita ilość wystawionych zwolnień lekarskich:"));
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                        $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Count(e => e.SickLeave))}"));
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Suma dni na zwolnieniach lekarskich:"));
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                        $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Examinations.Where(e => e.SickLeave).Sum(e => (e.SickLeaveTo - e.SickLeaveFrom).GetValueOrDefault().TotalDays))} dni"));
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Najdłuższe zwolnienie lekarskie:"));
                    globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1,
                        $"{patient.Hospitalizations.Max(hospitalization => hospitalization.Examinations.Max(e => (e.SickLeaveTo - e.SickLeaveFrom)?.TotalDays))} dni"));
                }
            }

            document.Add(globalInfoExaminationTable);
            document.Add(new Paragraph(""));
            document.Add(new LineSeparator(new SolidLine(2)));
            document.Add(new Paragraph(""));

            var globalInfoTreatmentTable = new Table(4);
            globalInfoTreatmentTable.AddCell(GenerateTextCell(1, 3, "Osoba była kiedykolwiek operowana:").SetBackgroundColor(Color.GRAY));
            globalInfoTreatmentTable.AddCell(GenerateTextCell(1, 1, BoolToYesNo(hasBeenThreatedInInstitution)));
            if (hasBeenThreatedInInstitution)
            {
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 3, "Całkowita ilość przeprowadzonych badań:"));
                globalInfoExaminationTable.AddCell(GenerateTextCell(1, 1, $"{patient.Hospitalizations.Sum(hospitalization => hospitalization.Treatments.Count)}"));
            }

            document.Add(globalInfoTreatmentTable);
            document.Add(new Paragraph(""));
            document.Add(new LineSeparator(new SolidLine(2)));
            document.Add(new Paragraph(""));
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

        private void GenerateNewHospitalizationInfo(Document document, HospitalizationTransferObject hospitalization)
        {
            var currentInstitutionName = _institutionRepository.Read(hospitalization.InstitutionId).InstitutionName;

            var hospitalizationBasicInfoTable = new Table(4);
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 4, $"{currentInstitutionName}").SetBackgroundColor(Color.GRAY).SetTextAlignment(TextAlignment.CENTER).SetFontSize(15));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 2, "Początek hospitalizacji:"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 2, $"{hospitalization.HospitalizationStartTime}"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 2, "Koniec hospitalizacji:"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 2, $"{hospitalization.HospitalizationEndTime}"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 4, "Wygenerowane dokumenty").SetBackgroundColor(Color.GRAY));
            hospitalizationBasicInfoTable.AddCell(GenerateListCell(1, 4, hospitalization.HospitalizationDocuments.Select(d => d.Name)));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 3, "Ilość badań podczas hospitalizacji:"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 1, $"{hospitalization.Examinations.Count}"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 3, "Ilość operacji podczas hospitalizacji:"));
            hospitalizationBasicInfoTable.AddCell(GenerateTextCell(1, 1, $"{hospitalization.Treatments.Count}"));

            document.Add(hospitalizationBasicInfoTable);

            document.Add(new Paragraph(""));
            document.Add(new LineSeparator(new SolidLine(2)));
            document.Add(new Paragraph(""));

            if(hospitalization.Examinations.Any())
                GenerateExaminationsTables(document, hospitalization.Examinations);

            if(hospitalization.Treatments.Any())
                GenerateTreatmentsTables(document, hospitalization.Treatments);
        }

        private void GenerateExaminationsTables(Document document, List<ExaminationTransferObject> examinations)
        {
            document.GetPdfDocument().AddNewPage();
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph("Przeprowadzone badania").SetFontSize(20));

            foreach (var examination in examinations.OrderBy(x => x.ExaminationStartTime))
            {
                var table = new Table(4);
                table.AddCell(GenerateTextCell(1, 1, "Początek badania:"));
                table.AddCell(GenerateTextCell(1, 1, $"{examination.ExaminationStartTime}"));
                table.AddCell(GenerateTextCell(1, 1, "Koniec badania:"));
                table.AddCell(GenerateTextCell(1, 1, $"{examination.ExaminationEndTime}"));
                table.AddCell(GenerateTextCell(1, 2, "Osoba badająca:"));
                table.AddCell(GenerateTextCell(1, 2, $"{examination.Examinator}"));
                table.AddCell(GenerateTextCell(1, 2, $"Wizyta prywatna:"));
                table.AddCell(GenerateTextCell(1, 2, $"{BoolToYesNo(examination.PrivateVisit)}"));
                table.AddCell(GenerateTextCell(1, 4, $"Użyte przyrządy").SetBackgroundColor(Color.GRAY));
                table.AddCell(GenerateListCell(4, 4, examination.UsedDevices.Split(',')));
                table.AddCell(GenerateTextCell(1, 2, "Wypisana recepta:"));
                table.AddCell(GenerateTextCell(1, 2, $"{BoolToYesNo(examination.SignedReceip)}"));
                table.AddCell(GenerateTextCell(1, 2, $"Zwolnienie lekarskie:"));
                table.AddCell(GenerateTextCell(1, 2, $"{BoolToYesNo(examination.SickLeave)}"));
                table.AddCell(GenerateTextCell(1, 1, $"Zwolnienie od:"));
                table.AddCell(GenerateTextCell(1, 1, $"{ValueOrNullReplacement(examination.SickLeaveFrom)}"));
                table.AddCell(GenerateTextCell(1, 1, $"Zwolnienie do:"));
                table.AddCell(GenerateTextCell(1, 1, $"{ValueOrNullReplacement(examination.SickLeaveTo)}"));
                table.AddCell(GenerateTextCell(4, 4, $"{examination.ExaminationDetails}"));

                document.Add(table);
                document.Add(new Paragraph(""));
                document.Add(new LineSeparator(new SolidLine(2)));
                document.Add(new Paragraph(""));
            }
        }

        private void GenerateTreatmentsTables(Document document, List<TreatmentTransferObject> treatments)
        {
            document.GetPdfDocument().AddNewPage();
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph("Przeprowadzone operacje:").SetFontSize(20));

            foreach (var treatment in treatments.OrderBy(x => x.TreatmentStartDate))
            {
                var table = new Table(4);
                table.AddCell(GenerateTextCell(1, 1, "Początek operacji:"));
                table.AddCell(GenerateTextCell(1, 1, $"{treatment.TreatmentStartDate}"));
                table.AddCell(GenerateTextCell(1, 1, "Koniec operacji:"));
                table.AddCell(GenerateTextCell(1, 1, $"{treatment.TreatmentEndDate}"));
                table.AddCell(GenerateTextCell(1, 4, "Zespół").SetBackgroundColor(Color.GRAY));
                table.AddCell(GenerateListCell(4, 4, treatment.Personel.Split(',')));
                table.AddCell(GenerateTextCell(1, 4, $"Użyte przyrządy").SetBackgroundColor(Color.GRAY));
                table.AddCell(GenerateListCell(4, 4, treatment.UsedDevices.Split(',')));
                table.AddCell(GenerateTextCell(1, 4, $"Użyte medykamenty").SetBackgroundColor(Color.GRAY));
                table.AddCell(GenerateListCell(4, 4, treatment.UsedMedicines.Select(m => $"{m.Medicine.MedicineName} - {m.Dose:F2}mg")));

                document.Add(table);
                document.Add(new Paragraph(""));
                document.Add(new LineSeparator(new SolidLine(2)));
                document.Add(new Paragraph(""));
            }
        }

        static Cell GenerateTextCell(int rows, int cols, string content)
        {
            return new Cell(rows, cols).Add(new Paragraph(content));
        }

        static Cell GenerateListCell(int rows, int cols, IEnumerable<string> listItems)
        {
            var list = new List();
            listItems.ForEach(l => list.Add(new ListItem(l)));
            return new Cell(rows, cols).Add(list);
        }

        static string BoolToYesNo(bool value)
        {
            return value ? "Tak" : "Nie";
        }

        static string ValueOrNullReplacement(DateTime? value)
        {
            return value?.ToShortDateString() ?? "nie dotyczy";
        }

        static string ValueOrNullReplacement(string value)
        {
            return string.IsNullOrEmpty(value) ? "nie dotyczy" : value;
        }
    }
}