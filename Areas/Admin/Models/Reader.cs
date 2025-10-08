
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.Areas.Admin.Models
{
	[Table("readers")]
	public class Reader
	{
		[Key]
		[Column("reader_id")] // 🔹 Đảm bảo đúng tên cột
		public int ReaderId { get; set; }

		
		[Required]
		[Column("library_card_number")]
		public string LibraryCardNumber { get; set; }

		[Column("phone")]
		public string Phone { get; set; }

		[Required]
		[Column("full_name")]
		public string FullName { get; set; }

		[Column("created_at")]
		public DateTime? CreatedAt { get; set; }

		[Column("updated_at")]
		public DateTime? UpdatedAt { get; set; }
	
	}
}
