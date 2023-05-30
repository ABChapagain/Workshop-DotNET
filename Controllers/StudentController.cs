using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly IClassInfoRepo _classInfoRepo;
    private readonly IStudentRepo _studentRepo;
    private readonly IfacultyRepo _facultyRepo;

    private readonly IUow _uow;
    public StudentController(IClassInfoRepo classInfoRepo, IStudentRepo studentRepo, IfacultyRepo facultyRepo, IUow uow)
    {
        _classInfoRepo = classInfoRepo;
        _studentRepo = studentRepo;
        _facultyRepo = facultyRepo;
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _studentRepo.GetQueryable()
        .Include(x => x.ClassInfo)
        .Include(x => x.Faculty)
        .OrderBy(x => x.Id).ToListAsync();
        return View(list);
    }

    public async Task<IActionResult> New()
    {
        return View(new StudentVm
        {
            ClassInfos = await _classInfoRepo.GetQueryable().ToListAsync(),
            Faculties = await _facultyRepo.GetQueryable().ToListAsync()
        });
    }


    [HttpPost]

    public async Task<IActionResult> New(StudentVm model)
    {
        try
        {
            var student = new Student()
            {
                Name = model.Name,
                Address = model.Address,
                ContactNo = model.ContactNo,
                ClassInfo = await _classInfoRepo.FindOrThrowAsync(model.ClassInfoId),
                Faculty = await _facultyRepo.FindOrThrowAsync(model.FacultyId)
            };

            await _studentRepo.AddAsync(student);
            await _uow.Commit();
            return RedirectToAction("Index");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



}