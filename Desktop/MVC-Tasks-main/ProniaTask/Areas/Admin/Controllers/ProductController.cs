using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProniaTask.DataAccesLayer;
using ProniaTask.Extensions;
using ProniaTask.ViewModels.Products;
namespace ProniaTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(ProniaContext _context, IWebHostEnvironment _env):Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Select(p => new GetProductAdminVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SellPrice = p.SellPrice,
                    StockCount = p.StockCount,
                    Raiting = p.Raiting,
                    Discount = p.Discount,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM data)
        {
            if (!data.ImageFile.IsValidType("image"))
                ModelState.AddModelError("ImageFile", "Fayl şəkil formatında olmalıdır.");
            if (data.ImageFile.IsValidLength(200))
                ModelState.AddModelError("ImageFile", "Faylın ölçüsü 200 kb-dan çox olmamalıdır.");
            //if (!ModelState.IsValid)
            //    return View(data);

            string fileName = await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));

            await _context.Products.AddAsync(new Models.Product
            {
                Name = data.Name,
                CostPrice = data.CostPrice,
                SellPrice = data.SellPrice,
                StockCount = data.StockCount,
                Raiting = data.Raiting,
                Discount = data.Discount,
                ImageUrl = Path.Combine("imgs", "products", fileName),
                isDeleted = false,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
