using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{ 
    public class Pago
    {
        [Key]
        [Display(Name = "Código")]
        public int IdPago { get; set; }
        [Required]
        public int IdContrato { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Monto { get; set; }

        // Propiedad de navegación
        public Contrato Contrato { get; set; }
    }
}