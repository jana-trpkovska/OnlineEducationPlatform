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
    }
}
