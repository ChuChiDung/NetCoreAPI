using Microsoft.AspNetCore.Mvc;
using MvcChuong6.Models;


namespace MvcChuong6.Controllers

{
    public class StudentController : Controller
    {
        public IActionResult ast()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ast(Student ps)
        {
            string strOutput = "Xin chao" + ps.TenHS +"-"+ ps.MaHS +"-"+ ps.Diem + "-" +ps.Lop;
            ViewBag.infoStudent = strOutput;
            return  View();
        }
    }
}