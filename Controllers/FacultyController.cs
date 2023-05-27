using FirstApp.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FacultyController : Controller
{
    private readonly AppDbContext _context;

    public FacultyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var list = await _context.Faculties.OrderBy(x => x.Id).ToListAsync();
        return View(list);
    }

    [HttpGet]

    public IActionResult Create()
    {
        return View(new FacultyVm());
    }

    [HttpPost]
    public async Task<IActionResult> Create(FacultyVm model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid Data");
            }

            var faculty = new Faculty()
            {
                Name = model.Name,
                Price = model.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Faculties.AddAsync(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]

    public async Task<IActionResult> Update(long id)
    {
        try
        {

            var faculty = await _context.Faculties.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (faculty == null) throw new Exception($"Faculty with id {id} not found");

            var model = new FacultyVm()
            {
                Name = faculty.Name,
                Price = faculty.Price
            };

            return View(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]

    public async Task<IActionResult> Update(long id, FacultyVm model)
    {
        try
        {
            var faculty = await _context.Faculties.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (faculty == null) throw new Exception($"Faculty with id {id} not found");

            faculty.Name = model.Name;
            faculty.Price = model.Price;

            _context.Faculties.Update(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var faculty = await _context.Faculties.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (faculty == null) throw new Exception($"Faculty with id {id} not found");

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}