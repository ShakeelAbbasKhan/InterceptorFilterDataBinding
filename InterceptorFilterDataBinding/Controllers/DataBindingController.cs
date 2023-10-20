using InterceptorFilterDataBinding.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterceptorFilterDataBinding.Controllers
{
    public class DataBindingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DataBindingController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDataById(int id)
        {

            var data = $"Data from source with ID: {id}";
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var data = _context.Categories.ToList();
            return Json(data);
        }

        [HttpPost]
        public IActionResult SaveData(Category category)
        {

            var data = _context.Add(category);
            _context.SaveChanges();
            return Json(category);
        }
    }
}
