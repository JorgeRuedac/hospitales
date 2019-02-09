using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("ingresos", Schema="")]
    public class ingresos
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Num Habitacion")]
        public Int32 Num_habitacion { get; set; }

        [StringLength(50)]
        [Display(Name = "Comentario")]
        public String Comentario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Ingreso")]
        public DateTime? Fecha_ingreso { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Salida")]
        public DateTime? Fecha_salida { get; set; }

        [Display(Name = "Num Cama")]
        public Int32? id_cama { get; set; }

        [Display(Name = "Historia")]
        public Int32? id_historia { get; set; }

        // ComboBox
        public virtual camas camas { get; set; }
        public virtual historia_clinica historia_clinica { get; set; }

    }
}
 
