using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Data;
using FinanceApp.Models;
using FinanceApp.Services;
using FinanceApp.Dtos;

namespace FinanceApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoriesService.AddAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, CategoryDTO categoryDto)
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            // if (id != categoryDto.Id)
            //     return NotFound();
            if (ModelState.IsValid)
            {
                await _categoriesService.EditAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}