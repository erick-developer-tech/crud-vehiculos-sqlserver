using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crud_vehiculos_sqlserver.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La matricula es obligatoria")]
        [StringLength(10, ErrorMessage = "Máximo de 10 caracteres")]
        [Display(Name = "Matricula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(2000, 2026, ErrorMessage = "El año debe ser entre 2000 y 2026")]
        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(50)]
        public string Color { get; set; }
    }
}