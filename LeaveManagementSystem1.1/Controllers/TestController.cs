using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem1._1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new Models.TestViewModel
            {
                Name = "Test Name",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            return View(data);
        }
    }
}
