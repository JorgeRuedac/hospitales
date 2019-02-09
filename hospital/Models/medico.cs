using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("medico", Schema="")]
    public class medico
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cod Medico")]
        public Int32 Cod_medico { get; set; }

        [StringLength(50)]
        [Display(Name = "Cedula")]
        public String Cedula { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public String Apellido_medico { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacimien")]
        public DateTime? Fecha_nacimien { get; set; }


    }
}
 
