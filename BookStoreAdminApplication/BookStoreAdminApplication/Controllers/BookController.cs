using BookStoreAdminApplication.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStoreAdminApplication.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportBooks(IFormFile file)
        {
            // string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";
            string pathToUpload = Path.Combine(Path.GetTempPath(), file.FileName);

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Book> users = getAllBooksFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "https://bookstoreweb20240912205458.azurewebsites.net/api/Admin/ImportAllBooks";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Home");

        }

        private List<Book> getAllBooksFromFile(string fileName)
        {
            List<Book> books = new List<Book>();
            //string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        books.Add(new Models.Book
                        {
                            Title = reader.GetValue(0).ToString(),
                            CoverImage = reader.GetValue(1).ToString(),
                            ReleaseYear = Convert.ToInt32(reader.GetDouble(2)),
                            ISBN = reader.GetValue(3).ToString(),
                            Price = reader.GetDouble(4),
                            Edition = Convert.ToInt32(reader.GetDouble(5)),
                            Genres = reader.GetValue(6).ToString().Trim('[', ']').Split(',')
                               .Select(g => (Models.Enums.GenreEnum)Enum.Parse(typeof(Models.Enums.GenreEnum), g.Trim()))
                               .ToList(),
                            AuthorId = Guid.Parse(reader.GetValue(7).ToString()),
                            PublisherId = Guid.Parse(reader.GetValue(8).ToString())
                        });
                    }

                }
            }
            return books;

        }

    }
}
