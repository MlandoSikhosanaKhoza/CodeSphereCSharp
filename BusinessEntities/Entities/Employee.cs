﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessEntities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(40)]
        public string Surname { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
