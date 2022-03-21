using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Models
{
    public class CustomerOrderPriceView
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public decimal Price { get; set; }
    }
}
