using Microsoft.AspNetCore.Mvc;
using MvcChuong6.Models;

 namespace MvcChuong6.Controllers
{
    public class FrutiController : Controller
    {
        public IActionResult HH()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HH(Fruti ps)
        {
            string strOutput = "Chao ban " + ps.Ten + "-" + ps.NamSinh + "-" + ps.ViTri;
            ViewBag.infoFruti = strOutput;
            return View();
            
        }

    }
}