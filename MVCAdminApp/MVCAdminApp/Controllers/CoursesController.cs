using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using MVCAdminApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVCAdminApp.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult ImportAllCourses()
        {
            return View();
        }

        public IActionResult ImportCourses(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Course> courses = getAllCoursesFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5291/api/Admin/ImportAllCourses";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(courses), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Home");
        }

        private List<Course> getAllCoursesFromFile(string fileName)
        {
            List<Course> courses = new List<Course>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        courses.Add(new Models.Course
                        {
                            Title = reader.GetValue(0).ToString(),
                            Description = reader.GetValue(1).ToString(),
                            Duration = Int32.Parse(reader.GetValue(2).ToString()),
                            Level = Int32.Parse(reader.GetValue(3).ToString()),
                            CourseImage = reader.GetValue(4).ToString()
                        });
                    }

                }
            }
            return courses;

        }
    }
}
