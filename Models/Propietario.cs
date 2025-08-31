using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{
    public class Propietario
    {
        [Key]
        [Display(Name = "Id")]
        public int IdPropietario { get; set; }
        [Required(ErrorMessage = "El campo dni es obligatorio"), MaxLength(10)]
        public string Dni { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio"), MaxLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio"), MaxLength(50)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo email es obligatorio"), DataType(DataType.EmailAddress), MaxLength(128)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo telefono es obligatorio"), MaxLength(15)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo clave es obligatorio"), DataType(DataType.Password), MaxLength(15)]
        public string Clave { get; set; }
        [Required(ErrorMessage = "El campo estado es obligatorio")]
        public bool Estado { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}