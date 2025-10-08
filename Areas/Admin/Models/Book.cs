using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.Areas.Admin.Models

	{
	public class Book
	{
		[Key]
		[Column("book_id")]
		public int Book_Id { get; set; }

		[Required]
		[StringLength(255)]
		[Column("title")]
		public string Title { get; set; }

		[Required]
		[StringLength(255)]
		[Column("author")]
		public string Author { get; set; }

		[StringLength(100)]
		[Column("category")]
		public string Category { get; set; }

		[StringLength(20)]
		[Column("isbn")]
		public string ISBN { get; set; }

		[Column("publication_year")]
		public int? PublicationYear { get; set; }

		[Required]
		[Column("storage_location")]
		public string? StorageLocation { get; set; }

		[Column("image")]
		public string? Image { get; set; }

		[Column("quantity")]
		
		public int? Quantity { get; set; }

		[Column("status")]
		public string? Status { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Column("updated_at")]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		[Column("describe")]
		public string? Describe { get; set; }

		[Column("price", TypeName = "DECIMAL(10,2)")]
		public decimal? Price { get; set; }
	
}

}


