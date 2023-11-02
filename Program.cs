using MemberDataEntryForm.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(10); });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddDbContext<MemberDataContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserAccess", policy => policy.RequireRole("OfficeClerk", "FrontDesk", "Receptionist"));
//});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        //options.LoginPath = "/User/Index"; // Set the login path
//        //options.AccessDeniedPath = "/User/Index"; // Set the access denied path
//    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=User}/{action=Login}/{id?}");


app.UseEndpoints(endpoints => // Use UseEndpoints for routing
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}");
});

app.Run();
