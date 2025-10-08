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
    public class HomeController : Controller
    {

		
		[Area("admin")]
		[Route("admin/home/index", Name ="trangchu")]
		public IActionResult Index()
		{
			// Kiểm tra nếu đã đăng nhập
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLoggedIn")))
			{
				return RedirectToAction("login", "account", new { area = "admin" });
			}

			// Lấy tên người dùng từ Session
			ViewBag.Username = HttpContext.Session.GetString("Username");

			return View();
		}

	}
}
