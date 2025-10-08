using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace LibraryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BorrowedBookController : Controller
	{
		private readonly AppDbContext _context;

		public class BorrowRequest
		{
			public int ReaderId { get; set; }
			public List<int> Books { get; set; } = new List<int>();
		}

		public BorrowedBookController(AppDbContext context)
		{
			_context = context;
		}

		[Route("admin/borrowedbook/borrowedbook", Name = "AdminBorrowedBook")]
		public IActionResult BorrowedBook()
		{
			return View();
		}

        // 📌 Tìm kiếm bạn đọc
        [HttpPost]
        [Route("admin/borrowedbook/searchreader")]
        public async Task<IActionResult> SearchReader(string LibraryCard)
        {
            var reader = await _context.Readers
                                       .FirstOrDefaultAsync(r => r.LibraryCardNumber == LibraryCard);

            if (reader == null)
            {
                return Json(new { success = false, message = "Không tìm thấy độc giả" });
            }

            return Json(new { success = true, reader });
        }


        // 📌 Tìm kiếm sách theo ISBN
        [HttpPost]
		[Route("admin/borrowedbook/searchbook")]
		public IActionResult SearchBook(string ISBN)
		{
			var book = _context.Books.FirstOrDefault(b => b.ISBN == ISBN);
			if (book == null)
			{
				return Json(new { success = false, message = "Không tìm thấy sách" });
			}
			return Json(new { success = true, book });
		}

        // 📌 Mượn sách (Xử lý nút "Mượn sách")
        [HttpPost]
        [Route("admin/borrowedbook/borrowbook")]
        public IActionResult BorrowBook([FromBody] BorrowRequest request)
        {
            // Kiểm tra xem ReaderId có hợp lệ không
            var reader = _context.Readers.Find(request.ReaderId);
            if (reader == null)
            {
                return Json(new { success = false, message = "Bạn đọc không hợp lệ" });
            }

            // Kiểm tra nếu không có sách nào được chọn
            if (request.Books == null || request.Books.Count == 0)
            {
                return Json(new { success = false, message = "Danh sách sách mượn trống" });
            }

            // Tạo mới một BorrowRecord
            var borrowRecord = new BorrowRecord
            {
                ReaderId = request.ReaderId,
                BorrowDate = DateTime.Now,
                Status = "Borrowed" // Trạng thái ban đầu là "Đang mượn"
            };

            _context.Borrow_Records.Add(borrowRecord);
            _context.SaveChanges();

            // Tạo BorrowDetail cho từng sách trong danh sách
            foreach (var bookId in request.Books)
            {
                var book = _context.Books.Find(bookId);
                if (book == null)
                    continue;

                // Tạo BorrowDetail cho từng sách
                var borrowDetail = new BorrowDetail
                {
                    BorrowRecordId = borrowRecord.BorrowId,
                    BookId = bookId,
                    TypeBorrow = BorrowType.Home, // Loại mượn mặc định là Home
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14), // Thời gian trả sách là 14 ngày
                    Status = BorrowStatus.Borrowed // Trạng thái ban đầu là "Đang mượn"
                };

                _context.BorrowDetails.Add(borrowDetail);
            }

            // Lưu lại thông tin vào cơ sở dữ liệu
            _context.SaveChanges();

            return Json(new { success = true, message = "Mượn sách thành công" });
        }


        // 📌 Xóa sách khỏi danh sách (Xử lý nút "Xóa")
        [HttpPost]
		[Route("admin/borrowedbook/DeleteBook")]
		public IActionResult RemoveBook(int BorrowDetailId)
		{
			var borrowDetail = _context.BorrowDetails.Find(BorrowDetailId);
			if (borrowDetail == null)
			{
				return Json(new { success = false, message = "Không tìm thấy sách" });
			}

			_context.BorrowDetails.Remove(borrowDetail);
			_context.SaveChanges();

			return Json(new { success = true, message = "Xóa sách khỏi danh sách mượn thành công" });
		}
	}
}
