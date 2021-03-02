using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNetExam.Models;
using dotNetExam.Services;

namespace dotNetExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILinkFinder linkFinder;
        
        public HomeController(ILinkFinder linkFinder)
        {
            this.linkFinder = linkFinder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Index(string link, int level, int count)
        {
            return View(await linkFinder.GetSitePages(link, level, count));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}