using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore; // Add this using directive


var builder = WebApplication.CreateBuilder(args);
// Thêm cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.UseStaticFiles(); // Đảm bảo phục vụ các file tĩnh như CSS, JS

// Route mặc định cho ứng dụng
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

// Route cho Areas (admin)
app.MapControllerRoute(
	name: "admin",
	pattern: "{area:exists}/{controller=AddBook}/{action=addbook}/{id?}");


// Chạy ứng dụng
app.Run();
