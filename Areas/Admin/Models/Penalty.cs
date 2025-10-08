using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Areas.Admin.Models
{
	public class Penalty
	{
		[Key]
		public int PenaltyId { get; set; }

		[ForeignKey("BorrowRecord")]
		public int? BorrowId { get; set; }
		public BorrowRecord BorrowRecord { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		public decimal? Fine { get; set; }

		[MaxLength(10)]
		public string Reason { get; set; }

		[MaxLength(10)]
		public string Status { get; set; }

		public DateTime? CreatedAt { get; set; }
	}
}
