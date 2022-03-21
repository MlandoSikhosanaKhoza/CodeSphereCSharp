using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Models
{
    public class CustomerOrderView
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public int NumOfOrders { get; set; }
    }
}
