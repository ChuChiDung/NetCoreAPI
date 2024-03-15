using Microsoft.AspNetCore.Mvc;
using MvcChuong6.Models;


namespace MvcChuong6.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Phan1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Phan1(EmployeeModel ps)
        {
            string strOutput = "Xin chao" + ps.MaNV +"-"+ ps.TenNV +"-"+ ps.Diachi + "-" +ps.Luong;
            ViewBag.infoEmployee = strOutput;
            return View("Phan1", ps);
            
        }
        
    }
}