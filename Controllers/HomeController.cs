using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstApp.Models;

namespace FirstApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]

    public IActionResult Index()
    {

        var list = new List<StudentVm>() {
            new StudentVm() {Id = 1, Name="Achyut Chapagain", Address="Surunga"},
            new StudentVm() {Id = 2, Name="Kamal Pahan", Address="Dhulabari"},
            new StudentVm() {Id = 3, Name="Dipesh Thapaliya", Address="Birtamode"}
        };
        return View(list);
    }

    public IActionResult About()
    {
        return View();
    }


    public IActionResult New()
    {
        return View(new TestVm());
    }

    [HttpPost]
    public IActionResult New(TestVm vm)
    {
        // return Content(vm.Name);
        return Ok(vm);

    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
