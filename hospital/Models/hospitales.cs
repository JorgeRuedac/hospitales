using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("hospitales", Schema="")]
    public class hospitales
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cod Hospital")]
        public Int32 Cod_hospital { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Ciudad")]
        public String Ciudad { get; set; }

        [StringLength(50)]
        [Display(Name = "Tlefono")]
        public String Tlefono { get; set; }

        [Display(Name = "Cod Medico")]
        public Int32? Cod_medico { get; set; }

        // ComboBox
        public virtual medico medico { get; set; }

    }
}
 
