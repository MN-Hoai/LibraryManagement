
using LibraryManagement.Areas.Admin.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BorrowRecord
{
	[Key]
	public int BorrowId { get; set; }

	[ForeignKey("Reader")]
	public int ReaderId { get; set; }
	public Reader Reader { get; set; }

	[ForeignKey("BorrowDetail")]
	public int? BorrowDetailId { get; set; }
	public BorrowDetail BorrowDetail { get; set; }

	public DateTime? BorrowDate { get; set; }

	[MaxLength(10)]
	public string Status { get; set; }

}


		