using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Service;
using FinanceApp.Models;
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
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.Add(expense);

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expensesService.GetExpenseById(id);
            if (expense == null)
                return NotFound();
            return View(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.Edit(expense);

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"DELETE ID: {id}");
            if (ModelState.IsValid)
            {
                await _expensesService.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult GetChart()
        {
            var data = _expensesService.GetChartData();
            return Json(data);
        }
    }
}