using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class CustomerLoginModel
    {
        [Required]
        [MaxLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
        [Remote("CheckIfMobileDoesntExist", "Customer",ErrorMessage ="Mobile number doesnt exist")]
        public string Mobile { get; set; }
    }
}
