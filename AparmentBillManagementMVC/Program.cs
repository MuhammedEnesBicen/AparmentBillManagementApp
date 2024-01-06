using Bussiness.Abstract;
using Bussiness.Concrete;
using DataAccess;
using DataAccess.Abstarct;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(opt =>
        {
            opt.LoginPath = "/Auth/Index";
            opt.AccessDeniedPath = "/Auth/AccessDenied";
        });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<DbContext, AppDbContext>();
builder.Services.AddSingleton<IApartmentDal, EfApartmentDal>();
builder.Services.AddSingleton<IApartmentService, ApartmentManager>();

builder.Services.AddSingleton<ITenantDal,EfTenantDal>();
builder.Services.AddSingleton<ITenantService, TenantManager>();

builder.Services.AddScoped<IManagerDal, EfManagerDal>();
builder.Services.AddScoped<IManagerService, ManagerManager>();

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

app.UseAuthentication();
app.UseAuthorization();




app.MapAreaControllerRoute(
       name: "TenantUserRoute",
       areaName: "TenantUser",
             pattern: "TenantUser/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");




app.Run();
