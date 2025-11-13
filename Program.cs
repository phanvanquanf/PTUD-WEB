using System.Net.NetworkInformation;
using aznews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
// Add services to the container.
builder.Services.AddControllersWithViews();
// Thêm dịch vụ MVC và Razor Pages

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

// Cấu hình phục vụ các tệp tĩnh trong thư mục assets/img
app.UseStaticFiles(new StaticFileOptions()
{
    // Cung cấp PhysicalFileProvider cho thư mục wwwroot/assets/img
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img") // Đảm bảo đường dẫn đúng
    ),
    // Thiết lập đường dẫn yêu cầu (URL) là /assets/img
    RequestPath = "/wwwroot/assets/img"
});


app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();  // Đảm bảo rằng các API controllers sẽ nhận request

app.UseStaticFiles(); // Static files if required (e.g., images, CSS, JS)


app.Run();
