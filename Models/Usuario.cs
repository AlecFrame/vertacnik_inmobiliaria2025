using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{ 

    public enum RolUsuario
    {
        Admin,
        Usuario
    } 
    public class Usuario
    {
        [Key]
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }
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
        [Required, DataType(DataType.Password), MaxLength(15)]
        public string Clave { get; set; }
        [Required]
        public RolUsuario Rol { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}