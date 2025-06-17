using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.Models;
using FinanceApp.Validators;
using FinanceApp.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MVC services
builder.Services.AddControllersWithViews();

// Configure PostgreSQL database
builder.Services.AddDbContext<FinanceAppContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Enable client-side validation adapters for FluentValidation
builder.Services.AddFluentValidationClientsideAdapters();

// Register FluentValidation validators
builder.Services.AddTransient<IValidator<ExpenseDTO>, ExpenseValidator>();
builder.Services.AddTransient<IValidator<CategoryDTO>, CategoryValidator>();

// Register AutoMapper with all profiles
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
