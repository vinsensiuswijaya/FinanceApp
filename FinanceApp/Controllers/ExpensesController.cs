using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.Models;
using FinanceApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        
        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _expensesService.GetAll();
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseDTO expenseDto)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.AddAsync(expenseDto);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expenseDto = await _expensesService.GetExpenseById(id);
            if (expenseDto == null)
                return NotFound();
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _expensesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetChart()
        {
            var data = await _expensesService.GetChartData();
            return Json(data);
        }
    }
}