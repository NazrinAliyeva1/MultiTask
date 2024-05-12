using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShopApplication.DataAccesLayer;
using MultiShopApplication.ViewModels.Categories;

namespace MultiShopApplication.Controllers
{
    public class HomeController:Controller
    {
        private readonly MultiShopDbContext _context;

        public HomeController(MultiShopDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var data = await _context.Categories.Where(c => c.Name.Length < 6).ToListAsync();
            //var data = await _context.Categories.Take(5).ToListAsync();
            //var data = await _context.Categories
            //    .OrderByDescending(x => x.Id)
            //    .Take(5)
            //    .ToListAsync();
            //return View(data);

            //return View(await _context.Categories.ToListAsync());


            var data = await _context.Categories
                .Where(x => x.isDeleted == false)
                .Select(x => new GetCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
            return View(data);

        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        public async Task<IActionResult> Test(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return Content(cat.Name);
        }

    }
}
