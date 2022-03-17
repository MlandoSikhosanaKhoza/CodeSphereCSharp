using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        [StringLength(150)]
        public string Country { get; set; }
        [Required]
        [StringLength(150)]
        public string Province { get; set; }
        [Required]
        [StringLength(150)]
        public string City { get; set; }
        [Required]
        [StringLength(150)]
        public string Suburb { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Unit nummber")]
        public string UnitNumber { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Complex name")]
        public string ComplexName { get; set; }
    }
}
