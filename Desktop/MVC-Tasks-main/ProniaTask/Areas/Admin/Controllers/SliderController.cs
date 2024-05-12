using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaTask.DataAccesLayer;
using ProniaTask.Models;
using ProniaTask.ViewModels.Sliders;
namespace ProniaTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(ProniaContext _context) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var data = await _context.Sliders.Select(s => new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImgUrl = s.ImgUrl,
                Subtitle= s.Subtitle,
                Title= s.Title,
            }).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(CreateSliderVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            //data'da problem yoxdursa bunu sql-e yuklemek isteyirik bunun ucun asagidaki kodu yaziriq.
            Slider slider = new Slider()
            {
                Discount=vm.Discount,
                CreateTime=DateTime.Now,
                ImgUrl=vm.ImgUrl,
                isDeleted=false,
                Subtitle=vm.Subtitle,
                Title=vm.Title,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
    public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync();
            if (slider is null) return NotFound();
            UpdateSliderVM sliderVM = new UpdateSliderVM
            {
                Discount = slider.Discount,
                Subtitle = slider.Subtitle,
                Title = slider.Title,
                ImgUrl = slider.ImgUrl
            };
             return View(sliderVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSliderVM sliderVM)
        {
            if (id == null || id < 1) return BadRequest();
            Slider existed = await _context.Sliders.FirstOrDefaultAsync(); ;
            existed.Title = sliderVM.Title;
            existed.Subtitle = sliderVM.Subtitle;
            existed.ImgUrl = sliderVM.ImgUrl;
            existed.Discount = sliderVM.Discount;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}
