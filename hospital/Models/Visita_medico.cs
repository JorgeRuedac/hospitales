using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("Visita_medico", Schema="")]
    public class Visita_medico
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cod Visita")]
        public Int32 Cod_visita { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [StringLength(50)]
        [Display(Name = "Hora")]
        public String Hora { get; set; }

        [StringLength(500)]
        [Display(Name = "Diagnostico")]
        public String Diagnostico { get; set; }

        [StringLength(500)]
        [Display(Name = "Tratamiento")]
        public String Tratamiento { get; set; }

        [Display(Name = "COD Hospital Servicio")]
        public Int32? ID_hospitales_servicios { get; set; }

        [Display(Name = "Historia")]
        public Int32? id_historia { get; set; }

        // ComboBox
        public virtual hospitales_servicios hospitales_servicios { get; set; }
        public virtual historia_clinica historia_clinica { get; set; }

    }
}
 
