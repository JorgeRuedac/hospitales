using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("historia_clinica", Schema="")]
    public class historia_clinica
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id Historia")]
        public Int32 id_historia { get; set; }

        [StringLength(50)]
        [Display(Name = "Cedula")]
        public String Cedula { get; set; }

        [StringLength(50)]
        [Display(Name = "Apellido")]
        public String Apellido { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacim")]
        public DateTime? Fecha_nacim { get; set; }

        [StringLength(50)]
        [Display(Name = "Num Seguridad Social")]
        public String Num_seguridad_social { get; set; }


    }
}
 
