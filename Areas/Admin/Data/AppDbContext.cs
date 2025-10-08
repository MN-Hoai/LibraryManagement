using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using LibraryManagement.Areas.Admin.Models;

namespace LibraryManagement.Data
{
	

	
		public class AppDbContext : DbContext
		{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Reader> Readers { get; set; }
		public DbSet<BorrowRecord> Borrow_Records { get; set; }
		public DbSet<BorrowDetail> BorrowDetails { get; set; }
		public DbSet<Penalty> Penalty { get; set; }


	}
	


}
