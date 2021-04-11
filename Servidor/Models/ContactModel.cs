using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
	public class ContactModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		public string Company { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Range(5, 10)]
		public long PhoneNumber { get; set; }
	}
}
