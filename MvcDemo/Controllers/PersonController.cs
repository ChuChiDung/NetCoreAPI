using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;



namespace MvcMovie.Controllers

{

public class PersonController : Controller

{

// GET: /Person/

public IActionResult Index()

{

return View();

}
[HttpPost]
public IActionResult Index(Person ps)
{
    string strOutput = "Xin chao" + ps.PersonID + "-" +ps.FullName + "-" +ps.Address;
    ViewBag.infoPerson = strOutput;
    return View();

}




}

}