using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using MVCAdminApp.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System.Text;

namespace MVCAdminApp.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5291/api/Admin/GetAllStudents";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Student>>().Result;
            return View(data);
        }

        public IActionResult Details(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5291/api/Admin/GetDetailsForStudent";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Student>().Result;
            return View(data);
        }

        public FileContentResult CreateInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5291/api/Admin/GetDetailsForStudent";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Student>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{StudentId}}", data.Id.ToString());
            document.Content.Replace("{{FirstName}}", data.FirstName);
            document.Content.Replace("{{LastName}}", data.LastName);
            document.Content.Replace("{{Email}}", data.Email);
            StringBuilder sb = new StringBuilder();

            foreach (var item in data.Enrollments)
            {
                sb.Append("Course " + item.Course.Title + " with duration " + item.Course.Duration + " with level " + item.Course.Level + "\n");
            }
            document.Content.Replace("{{CoursesList}}", sb.ToString());
            document.Content.Replace("{{DateEnrolled}}", data.DateEnrolled.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }

        [HttpGet]
        public FileContentResult ExportStudents()
        {
            string fileName = "AllStudentss.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Students");

                worksheet.Cell(1, 1).Value = "Student ID";
                worksheet.Cell(1, 2).Value = "Student Name";
                worksheet.Cell(1, 3).Value = "Student Surname";
                worksheet.Cell(1, 4).Value = "Student Email";
                worksheet.Cell(1, 5).Value = "Date Enrolled";

                HttpClient client = new HttpClient();
                string URL = "http://localhost:5291/api/Admin/GetAllStudents";
                HttpResponseMessage response = client.GetAsync(URL).Result;

                var data = response.Content.ReadAsAsync<List<Student>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var student = data[i];
                    worksheet.Cell(i + 2, 1).Value = student.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = student.FirstName;
                    worksheet.Cell(i + 2, 3).Value = student.LastName;
                    worksheet.Cell(i + 2, 4).Value = student.Email;
                    worksheet.Cell(i + 2, 5).Value = student.DateEnrolled.ToString();

                    for (int j = 0; j < student.Enrollments.Count(); j++)
                    {
                        worksheet.Cell(1, j + 6).Value = "Course - " + (j + 1);
                        worksheet.Cell(i + 2, j + 6).Value = student.Enrollments.ElementAt(j).Course.Title;

                    }
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }
    }
}
