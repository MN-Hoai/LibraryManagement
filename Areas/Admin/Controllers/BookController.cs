using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Areas.Admin.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace LibraryManagement.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BookController : Controller
	{
		private readonly AppDbContext _context;

		public BookController(AppDbContext context)
		{
			_context = context;
		}

		// Hiển thị danh sách sách và form thêm/sửa
		[HttpGet]
		[Route("admin/book/add", Name = "AdminAddBook")]
		public async Task<IActionResult> AddBook(int? id)
		{
			var books = await _context.Books.ToListAsync();
			Book book = id.HasValue ? await _context.Books.FindAsync(id) : new Book
			{
				Title = string.Empty,
				Author = string.Empty,
				Category = string.Empty,
				ISBN = string.Empty,
				StorageLocation = string.Empty,
				Quantity = null, // Giá trị mặc định
				PublicationYear = null, // Giá trị mặc định
				Describe = string.Empty,
				Price = 0m // Giá trị mặc định
			};
			return View(new BookViewModel { Books = books, Book = book });
		}

		// 📌 Xử lý thêm hoặc cập nhật sách
		[HttpPost]
		[Route("admin/book/add")]
		public async Task<IActionResult> AddBook([Bind("Book_Id,Title,Author,Category,ISBN,StorageLocation,PublicationYear,Quantity,Describe,Price,Image")] Book book, IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{
				// Kiểm tra nếu PublicationYear bị null hoặc rỗng
				if (!book.PublicationYear.HasValue)
				{
					book.PublicationYear = null; // Hoặc đặt giá trị mặc định nếu cần
				}

				// Nếu có ảnh mới được upload thì xử lý lưu ảnh và cập nhật thuộc tính Image
				if (ImageFile != null)
				{
					var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpLoad");
					if (!Directory.Exists(uploadsFolder))
						Directory.CreateDirectory(uploadsFolder);

					var fileName = Path.GetFileName(ImageFile.FileName);
					var filePath = Path.Combine(uploadsFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await ImageFile.CopyToAsync(stream);
					}

					book.Image = "/FileUpLoad/" + fileName;
				}
				
					book.UpdatedAt = DateTime.Now;

				if (book.Book_Id == 0) // Thêm mới sách
				{
					book.CreatedAt = DateTime.Now;
					_context.Books.Add(book);
				}
				else // Cập nhật sách
				{
					var existingBook = await _context.Books.FindAsync(book.Book_Id);
					if (existingBook != null)
					{
						existingBook.Title = book.Title;
						existingBook.Author = book.Author;
						existingBook.Category = book.Category;
						existingBook.PublicationYear = book.PublicationYear;
						existingBook.ISBN = book.ISBN;
						existingBook.StorageLocation = book.StorageLocation;
						existingBook.Quantity = book.Quantity;
						existingBook.Price = book.Price;
						existingBook.Describe = book.Describe;
						existingBook.UpdatedAt = DateTime.Now;
						existingBook.Image = book.Image;
						
					}
				}

				await _context.SaveChangesAsync();
				return RedirectToRoute("AdminAddBook");
			}

			// Nếu dữ liệu không hợp lệ, load lại view với model hiện tại
			var books = await _context.Books.ToListAsync();
			return View(new BookViewModel { Books = books, Book = book });
		}

		//  Xóa sách 
		[HttpPost]
		[Route("admin/book/delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return Json(new { success = false, message = "Không tìm thấy sách." });
			}

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return Json(new { success = true, message = "Xóa sách thành công." });
		}
	}

	public class BookViewModel
	{
		public List<Book> Books { get; set; }
		public Book Book { get; set; }
	}
}
