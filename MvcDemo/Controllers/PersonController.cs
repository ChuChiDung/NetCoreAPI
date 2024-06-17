using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcDemo.Data;
using MvcDemo.Models;
using X.PagedList;

using MvcDemo.Models.Process;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace MvcDemo.Controllers

{
    

public class PersonController : Controller

{
    private readonly ApplicationDbContext _context;
    private ExcelProcess _excelProcess = new ExcelProcess();
    public PersonController (ApplicationDbContext context) 
    {
        _context = context;
    }
    public async Task<IActionResult> Index(int? page, int? PageSize) 
    {
        ViewBag.PageSize = new List<SelectListItem>()
        {
            new SelectListItem { Value="3", Text="3"},
            new SelectListItem { Value="5", Text="5"},
            new SelectListItem { Value="10", Text="10"},
            new SelectListItem { Value="15", Text="15"},
            new SelectListItem { Value="25", Text="25"},
            new SelectListItem { Value="50", Text="50"},
        };
        int pagesize = (PageSize ?? 3);
        ViewBag.psize = pagesize;

        var model =  _context.Person.ToList().ToPagedList(page?? 1 , pagesize);
        return View(model);
    }
    public IActionResult Create() 
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create ([Bind("PersonID,FullName,Address")]Person person) 
    {
        if (ModelState.IsValid)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }
    public async Task<IActionResult> Edit(String id) 
    {
        if (id == null || _context.Person == null)
        {
            return NotFound();
        }

        var person = await _context.Person.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return View(person);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("PersonID,FullName,Address")] Person person) 
    {
        if (id != person.PersonID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }
    public async Task<IActionResult> Delete(string id) 
    {
        if (id == null || _context.Person == null)
        {
            return NotFound();
        }

        var person = await _context.Person
            .FirstOrDefaultAsync(m => m.PersonID == id);
        if (person == null)
        {
            return NotFound();
        }    
        return View(person);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id) 
    {
        if (_context.Person == null)
        {
            return Problem("Entity set 'AppLicationDbContext.Person' is null.");
        }
        var person = await _context.Person.FindAsync(id);
        if (person != null)
        {
            _context.Person.Remove(person);
        }

        await _context.SaveChangesAsync();
        return  RedirectToAction(nameof(Index));
    }
    private bool PersonExists(string id)    
    {
        return (_context.Person?.Any(e => e.PersonID == id)).GetValueOrDefault();
    }

    public async Task<IActionResult>Upload()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Upload(IFormFile file)
    {
        if (file!=null)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension !=".xls" && fileExtension != ".xls")
            {
                ModelState.AddModelError("", "Please choose excel file to upload!");
            }
            else
            {
                //rename file when upload to server
                var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream =new FileStream(filePath, FileMode.Create))
                {
                    //save file to server
                    await file.CopyToAsync(stream);
                    // read data from excel file fill DataTable
                    var dt = _excelProcess.ExcelToDataTable(fileLocation);
                    //using for loop to read data from dt
                    for (int i=0; i< dt.Rows.Count; i++)
                    {
                        //create new Peson object
                        var ps = new Person();
                        //set value to attributes
                        ps.PersonID = dt.Rows[i][0].ToString();
                        ps.FullName = dt.Rows[i][1].ToString();
                        ps.Address = dt.Rows[i][3].ToString();
                        //add object to context
                        _context.Add(ps);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }  
            }
        }
        return View ();
    }
        public IActionResult Download()
        {
            //Name the file when downloading
            var fileName = "YouFileName" + ".xlsx";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                //add some text to cell A1
                worksheet.Cells["A1"].Value = "PersonID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";
                //get all Person
                var personList = _context.Person.ToList();
                //fill data to worksheet
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                //dowload file
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }


    }

}