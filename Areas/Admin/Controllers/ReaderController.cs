using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Areas.Admin.Models;
using System.Threading.Tasks;
using System.Linq;

namespace LibraryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ReaderController : Controller
	{
		private readonly AppDbContext _context;

		public ReaderController(AppDbContext context)
		{
			_context = context;
		}

		// Hiển thị danh sách bạn đọc và form thêm/sửa
		[HttpGet]
		[Route("admin/reader/accountcreate", Name = "AdminAddReader")]
		public async Task<IActionResult> AccountCreate(int? id)
		{
			var readers = await _context.Readers.ToListAsync();
			var reader = id.HasValue ? await _context.Readers.FindAsync(id) : new Reader();

			return View(new ReaderViewModel { Readers = readers, Reader = reader });
		}

		// Xử lý thêm hoặc cập nhật bạn đọc
		[HttpPost]
		[Route("admin/reader/accountcreate", Name = "AdminAddReader")]
	
		public async Task<IActionResult> AccountCreate([FromBody] Reader reader)
		{
			if (ModelState.IsValid)
			{
				reader.UpdatedAt = DateTime.Now;

				if (reader.ReaderId == 0) // Thêm mới
				{
					reader.CreatedAt = DateTime.Now;
					_context.Readers.Add(reader);
				}
				else // Cập nhật
				{
					var existingReader = await _context.Readers.FindAsync(reader.ReaderId);
					if (existingReader != null)
					{
						existingReader.FullName = reader.FullName;
						existingReader.LibraryCardNumber = reader.LibraryCardNumber;
						existingReader.Phone = reader.Phone;
						existingReader.UpdatedAt = DateTime.Now;
					}
				}

				await _context.SaveChangesAsync();
				return Json(new { success = true, message = "Lưu thành công!" });
			}

			return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
		}


		// Xóa bạn đọc
		[HttpPost]
		[Route("admin/reader/delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			var reader = await _context.Readers.FindAsync(request.Id);
			if (reader == null)
			{
				return Json(new { success = false, message = "Không tìm thấy bạn đọc." });
			}

			_context.Readers.Remove(reader);
			await _context.SaveChangesAsync();

			return Json(new { success = true, message = "Xóa bạn đọc thành công." });
		}
		[HttpGet]
		[Route("admin/reader/checkLibraryCard")]
		public async Task<IActionResult> CheckLibraryCard(string cardNumber)
		{
			bool exists = await _context.Readers.AnyAsync(r => r.LibraryCardNumber == cardNumber);
			return Json(new { exists });
		}
	}

	public class ReaderViewModel
	{
		public List<Reader> Readers { get; set; }
		public Reader Reader { get; set; }
	}

	public class DeleteRequest
	{
		public int Id { get; set; }
	}
}
