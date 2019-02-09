using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace hospital.Models
{
    [Table("hospitales_servicios", Schema="")]
    public class hospitales_servicios
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "I D Hospitales Servicios")]
        public Int32 ID_hospitales_servicios { get; set; }

        [Display(Name = "Cod Hospital")]
        public Int32? Cod_hospital { get; set; }

        [Display(Name = "Id Servicio")]
        public Int32? Id_servicio { get; set; }

        [StringLength(50)]
        [Display(Name = "Codigo Refer")]
        public String CodigoRefer { get; set; }

        // ComboBox
        public virtual hospitales hospitales { get; set; }
        public virtual servicios servicios { get; set; }

    }
}
 
