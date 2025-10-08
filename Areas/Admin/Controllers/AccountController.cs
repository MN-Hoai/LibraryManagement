
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Areas.Admin.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace LibraryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly AppDbContext _context;

		public AccountController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Hiển thị trang đăng nhập
		[HttpGet]
		[Route("admin/account/login", Name = "AdminLogin")]
		public IActionResult Login()
		{
			return View();
		}

		// POST: Xử lý đăng nhập
		[HttpPost]
		[Route("admin/account/login", Name = "AdminLogin")]
		public async Task<IActionResult> Login(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
				return View();
			}
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
			if (user != null)
			{
				// Lưu thông tin vào session
				HttpContext.Session.SetString("AdminLoggedIn", "true");
				HttpContext.Session.SetString("Username", user.Username);

				// Chuyển đến trang Dashboard
				return RedirectToAction("Index", "Home", new { area = "Admin" });
			}
			else
			{
				ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
				return View();
			}
		}

		// Đăng xuất
		[Route("admin/account/logout")]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
