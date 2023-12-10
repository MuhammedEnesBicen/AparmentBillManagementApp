using Bussiness.Abstract;
using Bussiness.Automapper;
using Bussiness.Concrete;
using DataAccess;
using DataAccess.Abstarct;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<DbContext, AppDbContext>();
builder.Services.AddSingleton<IApartmentDal, EfApartmentDal>();
builder.Services.AddSingleton<IApartmentService, ApartmentManager>();

builder.Services.AddSingleton<ITenantDal,EfTenantDal>();
builder.Services.AddSingleton<ITenantService, TenantManager>();

builder.Services.AddSingleton<IBillDal, EfBillDal>();
builder.Services.AddSingleton<IBillService, BillManager>();


//builder.Services.AddSingleton<MappingProfile>();

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
    pattern: "{controller=Apartment}/{action=Index}/{id?}");

app.Run();
