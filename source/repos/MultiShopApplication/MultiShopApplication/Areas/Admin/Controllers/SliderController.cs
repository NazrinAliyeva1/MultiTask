using Microsoft.AspNetCore.Mvc;
using MultiShopApplication.DataAccesLayer;
using MultiShopApplication.Models;
using MultiShopApplication.ViewModels.Sliders;

namespace MultiShopApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(MultiShopDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Discount = vm.Discount,
                CreatedTime = DateTime.Now,
                ImgUrl = vm.ImgUrl,
                isDeleted = false,
                Subtitle = vm.Subtitle,
                Title = vm.Title
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return View();

        }
    }
}
