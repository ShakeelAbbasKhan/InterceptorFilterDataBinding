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
        public IActionResult GetDataById([FromRoute] int id)
        {

            var data = $"Data from source with ID: {id}";
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = _context.Categories.ToList();
          //  Task.Delay(10000).Wait();
            return Json(data);
        }

        [HttpPost]
        public IActionResult SaveData([FromBody] Category category)
        {

            var data = _context.Add(category);
            _context.SaveChanges();
            return Json(category);
        }

        [HttpPost]
        public IActionResult SaveDataByForm([FromForm] Category category)
        {
            var data = _context.Add(category);
            _context.SaveChanges();

            return Json(category);
        }

        [HttpDelete]
        public IActionResult DeleteData([FromQuery] int id)
        {
            var dataToDelete = _context.Categories.FirstOrDefault(u => u.Id == id);
            if (dataToDelete != null)
            {
                _context.Categories.Remove(dataToDelete);
                _context.SaveChanges();
                return Json("Data deleted successfully.");
            }

            return Json("Data not found or could not be deleted.");
        }


    }
}
