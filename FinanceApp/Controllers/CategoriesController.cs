using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Data;
using FinanceApp.Models;
using FinanceApp.Services;
using FinanceApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinanceApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var categories = await _categoriesService.GetAllByUserIdAsync(userId);
            categories = categories.OrderBy(c => c.Id);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                await _categoriesService.AddAsync(categoryDto, userId);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();
            var category = await _categoriesService.GetByIdAsync(id, userId);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                await _categoriesService.EditAsync(categoryDto, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            await _categoriesService.DeleteAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}