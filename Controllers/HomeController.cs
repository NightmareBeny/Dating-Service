using DatingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using DatingService.Data;

namespace DatingService.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatingServiceContext _context;

        public HomeController(DatingServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            ViewBag.Error = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string login, string password)
        {
            var client = await _context.Client.FindAsync(login);
            var emp = await _context.Employee.FindAsync(login);
            if (client != null && client.Password == password) return RedirectToAction("Profile", "Clients", new { id = login });
            else if (emp != null && emp.Password == password) return RedirectToAction("Details", "Employees", new { id = login });
            else ViewBag.Error = true;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
