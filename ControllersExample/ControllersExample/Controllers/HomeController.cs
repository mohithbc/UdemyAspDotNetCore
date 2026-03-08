using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;

namespace ControllersExample.Controllers
{
    [Controller] // optional, only ig u dont suffix 
    public class HomeController: Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            return Content("Hello from index", "text/plain");
        }

        [Route("about")]
        public string About()
        {
            return "Hello from about";
        }

        [Route("person")]
        public JsonResult Person(int age)// Getting age from user
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Smith",
                Age = age
            };
            return new JsonResult(person);
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
           // return new VirtualFileResult("/BC_MOHITH_RESUME.pdf", "application/pdf");

           // instead of return new we can do as below
           return File("/BC_MOHITH_RESUME.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            return new PhysicalFileResult(@"C:\Users\mohit\OneDrive\Desktop\MyDocs\MOHITH_BC_Resume.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\mohit\OneDrive\Desktop\MyDocs\MOHITH_BC_Resume.pdf");
            return new FileContentResult(bytes, "application/pdf");
        }

        [Route("contact-us")]
        public string Contact()
        {
            return "Hello from contact";
        }
    }
}
