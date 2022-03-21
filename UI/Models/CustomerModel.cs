﻿using BusinessEntities;
using Microsoft.AspNetCore.Mvc;
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
        [Remote("CheckIfMobileExist", "Customer", ErrorMessage = "Mobile number already exists",AdditionalFields = "CustomerId")]
        public string Mobile { get; set; }
    }
    public class CustomerHistoryModel : CustomerModel
    {
        public List<Order> OngoingOrders { get; set; }
        public List<Order> CompleteOrders { get; set; }
    }
}
