using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Areas.Admin.Models
{
	public enum BorrowType
	{
		Home,  // Ngắn hạn
		Here    // Dài hạn
	}

	public enum BorrowStatus
	{
		Borrowed,   // Đang mượn
		Returned,   // Đã trả
		Overdue     // Quá hạn
	}

	public class BorrowDetail
	{
		[Key]
		public int BorrowDetailId { get; set; }

		[ForeignKey("Book")]
		public int? BookId { get; set; }
		public Book Book { get; set; }

		[ForeignKey("BorrowRecord")]
		public int? BorrowRecordId { get; set; }
		public BorrowRecord BorrowRecord { get; set; }

		public BorrowType TypeBorrow { get; set; }

		public DateTime? BorrowDate { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? ReturnDate { get; set; }

		public BorrowStatus Status { get; set; }
	}
}
