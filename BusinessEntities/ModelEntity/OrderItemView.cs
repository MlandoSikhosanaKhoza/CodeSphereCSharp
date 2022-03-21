using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Models
{
    public class OrderItemView
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
