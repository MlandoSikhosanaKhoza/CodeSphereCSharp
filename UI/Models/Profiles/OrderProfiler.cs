using AutoMapper;
using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Profiles
{
    public class OrderProfiler:Profile
    {
        public OrderProfiler()
        {
            CreateMap<OrderModel, Order>();
            CreateMap<Order, OrderModel>();
        }
    }
}
