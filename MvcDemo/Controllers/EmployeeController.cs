using Microsoft.AspNetCore.Mvc;

namespace MvcDemo.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}