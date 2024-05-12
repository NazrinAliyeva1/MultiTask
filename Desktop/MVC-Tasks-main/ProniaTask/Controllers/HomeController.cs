using Microsoft.AspNetCore.Mvc;
using ProniaTask.DataAccesLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProniaTask.ViewModels.Categories;
using ProniaTask.ViewModels.Sliders;

namespace ProniaTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProniaContext _context;

        public HomeController(ProniaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var data= await _context.Categories.Where(c=>c.Name.Length<5).ToListAsync();
            //var data= await _context.Categories.Take(5).ToListAsync();
            //var data= await _context.Categories
            //    .OrderByDescending(x=>x.Id)
            //    .Take(5)
            //    .ToListAsync();
            //return View(data);

            var data = await _context.Sliders.Select(s => new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImgUrl = s.ImgUrl,
                Subtitle = s.Subtitle,
                Title = s.Title,
            }).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Test(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            var cat = await _context.Categories.FindAsync(id);
            if (id == null) return NotFound();
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return Content(cat.Name);
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
    }
}
