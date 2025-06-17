using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.Models;
using FinanceApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly ICategoriesService _categoriesService;

        public ExpensesController(IExpensesService expensesService, ICategoriesService categoriesService)
        {
            _expensesService = expensesService;
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(string sortOrder = "date")
        {
            ViewData["DescriptionSortParm"] = String.IsNullOrEmpty(sortOrder) ? "description" : "";
            ViewData["AmountSortParm"] = sortOrder == "amount" ? "amount_desc" : "amount";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";

            var expenses = await _expensesService.GetAllAsync();
            switch (sortOrder)
            {
                case "description_desc":
                    expenses = expenses.OrderByDescending(e => e.Description);
                    break;
                case "description":
                    expenses = expenses.OrderBy(e => e.Description);
                    break;
                case "amount_desc":
                    expenses = expenses.OrderByDescending(e => e.Amount);
                    break;
                case "amount":
                    expenses = expenses.OrderBy(e => e.Amount);
                    break;
                case "date":
                    expenses = expenses.OrderBy(e => e.Date);
                    break;
                case "date_desc":
                    expenses = expenses.OrderByDescending(e => e.Date);
                    break;
                default:
                    expenses = expenses.OrderBy(e => e.Id);
                    break;
            }
            return View(expenses);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var model = new ExpenseDTO
            {
                Date = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseDTO expenseDto)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.AddAsync(expenseDto);

                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", expenseDto.CategoryId);
            return View(expenseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expenseDto = await _expensesService.GetByIdAsync(id);
            if (expenseDto == null)
                return NotFound();

            expenseDto.Date = expenseDto.Date.ToLocalTime();

            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", expenseDto.CategoryId);
            return View(expenseDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseDTO expenseDto)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.EditAsync(expenseDto);

                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", expenseDto.CategoryId);
            return View(expenseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _expensesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetChart()
        {
            var data = await _expensesService.GetChartDataAsync();
            return Json(data);
        }
    }
}