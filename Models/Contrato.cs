using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{ 
    public class Contrato
    {
        [Key]
        [Display(Name = "Código")]
        public int IdContrato { get; set; }
        [Required]
        public int IdInquilino { get; set; }
        [Required]
        public int IdInmueble { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Monto { get; set; }

        // Propiedades de navegación
        public Inquilino Inquilino { get; set; }
        public Inmueble Inmueble { get; set; }
    }
}