using J2N.Text;
using System.ComponentModel.DataAnnotations;

namespace Fixxo.Models
{
	public class ContactFormModel
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		public string Comment { get; set; } = null!;

		public string? RedirectUrl { get; set; } = "/";
	}
}
