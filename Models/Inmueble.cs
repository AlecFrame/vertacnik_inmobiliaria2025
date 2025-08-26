using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace vertacnik_inmobiliaria2025.Models
{
    [Table("Inmuebles")]
    public class Inmueble
    {
        [Key]
        [Display(Name = "Id")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo precio es obligatorio"), DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El campo uso es obligatorio")]
        public string Uso { get; set; }

        [Required(ErrorMessage = "El campo ambiente es obligatorio")]
        public int Ambiente { get; set; }

        [Required(ErrorMessage = "El campo superficie es obligatorio")]
        public int Superficie { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "El campo estado es obligatorio")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El campo propietario es obligatorio")]
        [Display(Name = "Propietario")]
        public int IdPropietario { get; set; }

        [ForeignKey("IdPropietario")]
        public Propietario Propietario { get; set; }

        public override string ToString()
        {
            return $"En {Direccion}, Precio: {Precio:C}, Estado: {Estado}, Propietario: {Propietario}";
        }
    }
}