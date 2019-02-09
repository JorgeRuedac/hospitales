using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("servicios", Schema="")]
    public class servicios
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id Servicio")]
        public Int32 Id_servicio { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre Servicio")]
        public String Nombre_servicio { get; set; }


    }
}
 
