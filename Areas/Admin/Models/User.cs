using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.Areas.Admin.Models

	{
	public class User
	{
		[Key]
		[Column("user_id")]
		public int User_Id { get; set; }
		[Required]
		[Column("username")]
		public required string Username { get; set; }
		[Required]
		[Column("password")]
		public required string Password { get; set; }
		[Required]
		[Column("email")]
		public required string Email { get; set; }

	}

}


