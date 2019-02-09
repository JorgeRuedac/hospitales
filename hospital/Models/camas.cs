using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("camas", Schema="")]
    public class camas
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id Cama")]
        public Int32 id_cama { get; set; }

        [Display(Name = "Num Cama")]
        public Int32? Num_cama { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado")]
        public String Estado { get; set; }

        [Display(Name = "COD Hospital Servicio")]
        public Int32? ID_hospitales_servicios { get; set; }

        // ComboBox
        public virtual hospitales_servicios hospitales_servicios { get; set; }

    }
}
 
