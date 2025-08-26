using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{
	public class Inquilino
	{
		[Key]
		[Display(Name = "Código")]
		public int IdInquilino { get; set; }
		[Required, MaxLength(50)]
		public string Nombre { get; set; }
		[Required, MaxLength(50)]
		public string Apellido { get; set; }
		[Required, MaxLength(10)]
		public string Dni { get; set; }
        [MaxLength(15)]
		public string Telefono { get; set; }
		[Required, EmailAddress, MaxLength(128)]
		public string Email { get; set; }
	}
}
