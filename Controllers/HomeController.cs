using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Areas.Admin.Models;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

public class HomeController : Controller
{
	private readonly AppDbContext _context;

	public HomeController(AppDbContext context)
	{
		_context = context;
	}
	[Route("/", Name = "Default")]
	public IActionResult Index()
    {
        return View();
    }
	[Route("home/borrwedList", Name ="DanhSachMuon")]
	public IActionResult BorrowedList()
	{
		return View();
	}
	[HttpGet("/home/getCategories")]
	public async Task<IActionResult> GetCategories()
	{
		var categories = await _context.Books
			.Where(b => !string.IsNullOrEmpty(b.Category))
			.Select(b => b.Category)
			.Distinct()
			.ToListAsync();

		return Json(categories);
	}

	[HttpGet("/home/searchBooks")]
	public async Task<IActionResult> SearchBooks(string keyword)
	{
		var books = await _context.Books
			.Where(b => b.Title.Contains(keyword) ||
						b.Author.Contains(keyword) ||
						b.Category.Contains(keyword) ||
						b.ISBN.Contains(keyword) ||
						b.PublicationYear.ToString().Contains(keyword) ||
						b.Describe.Contains(keyword))
						
			.Select(b => new {
				b.Book_Id,
				b.Title,
				b.Author,
				b.Category,
				b.ISBN,
				b.PublicationYear,
				b.Price
			})
			.ToListAsync();

		return Json(books);
	}

	[HttpGet("/home/bookdetails/{id}")]
	[Route("home/bookdetails", Name = "ChiTietSach")]
	public async Task<IActionResult> BookDetails(int id)
	{
		var book = await _context.Books.FindAsync(id);
		if (book == null)
		{
			return NotFound();
		}
		return View(book);
	}


}
