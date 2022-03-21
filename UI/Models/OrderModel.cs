using BusinessEntities;
using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid OrderReference { get; set; }
        public decimal VAT { get; set; } = Tax.VAT;
        public decimal Subtotal { get; set; }
        public decimal GrandTotal { get; set; }

        public List<Item> Items { get; set; }
        public List<OrderItemsViewModel> OrderItemsView { get; set; }
    }
    public class OrderItemsViewModel:OrderItem
    {
        public string Description { get; set; }
    }
}
