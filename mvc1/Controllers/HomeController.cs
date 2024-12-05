using Microsoft.AspNetCore.Mvc;
using mvc1.Models;

namespace mvc1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string message;
        public HomeController(IRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            repository = repo;
            _httpContextAccessor = httpContextAccessor;
            var hostName = _httpContextAccessor.HttpContext.Request.Host.Value;
            message = $"Docker - ({hostName})";
        }
        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(repository.Produtos);
        }
    }
}
