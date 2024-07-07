using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using MVCAdminApp.Models;
using MVCAdminApp.Models.PartneringAppModels;
using Newtonsoft.Json;
using System.Text;

namespace MVCAdminApp.Controllers
{
    public class PartneringAppController : Controller
    {

        public PartneringAppController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://bookstoreweb20240707160345.azurewebsites.net/api/Admin/GetAllBooks";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Books>>().Result;
            return View(data);
        }

        public IActionResult Details(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://bookstoreweb20240707160345.azurewebsites.net/api/Admin/GetBooksDetails";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Books>().Result;
            return View(data);
        }

        public FileContentResult PrintBookDetails(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://bookstoreweb20240707160345.azurewebsites.net/api/Admin/GetBooksDetails";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Books>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "PartneringInvoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{BookTitle}}", data.Title.ToString());
            document.Content.Replace("{{BookPrice}}", data.Price.ToString());
            document.Content.Replace("{{ReleaseDate}}", data.ReleaseDate.ToString());
            document.Content.Replace("{{Author}}", data.Author.FirstName.ToString() + " " + data.Author.LastName);

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "BookDetails.pdf");

        }

        public IActionResult ExportAllBooks()
        {
            string fileName = "AllBooks.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Books");
                worksheet.Cell(1, 1).Value = "Title";
                worksheet.Cell(1, 2).Value = "Price";
                worksheet.Cell(1, 3).Value = "Release date";
                worksheet.Cell(1, 4).Value = "Author";
                HttpClient client = new HttpClient();
                string URL = "https://bookstoreweb20240707160345.azurewebsites.net/api/Admin/GetAllBooks";

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<Books>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.Title.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Price.ToString();
                    worksheet.Cell(i + 2, 3).Value = item.ReleaseDate.ToString();
                    worksheet.Cell(i + 2, 4).Value = item.Author.FirstName.ToString() + " " + item.Author.LastName;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

    }
}
