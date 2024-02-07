using Bussiness.Abstract;
using Bussiness.Concrete;
using DataAccess;
using DataAccess.Abstract;
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

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IApartmentDal, EfApartmentDal>();
builder.Services.AddScoped<IApartmentService, ApartmentManager>();

builder.Services.AddScoped<ITenantDal, EfTenantDal>();
builder.Services.AddScoped<ITenantService, TenantManager>();

builder.Services.AddScoped<IManagerDal, EfManagerDal>();
builder.Services.AddScoped<IManagerService, ManagerManager>();

builder.Services.AddScoped<IBillDal, EfBillDal>();
builder.Services.AddScoped<IBillService, BillManager>();

builder.Services.AddScoped<IMessageDal, EfMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<IChatRoomDal, EfChatRoomDal>();
builder.Services.AddScoped<IChatRoomService, ChatRoomManager>();


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
    pattern: "{controller=Auth}/{action=LandingPage}/{id?}");

//make tenantuser area default
//app.MapGet("/", (HttpContext context) =>
//{
//    context.Response.Redirect("/Message/Index");
//    return Task.CompletedTask;
//});


app.Run();
