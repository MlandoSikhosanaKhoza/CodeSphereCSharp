using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name ="Item name")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Price without VAT")]
        public decimal Price { get; set; }
    }
}
