using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(11)]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Only numbers allowed")]
        public string Mobile { get; set; }

        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
