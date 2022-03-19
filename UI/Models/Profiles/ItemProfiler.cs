using AutoMapper;
using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Profiles
{
    public class ItemProfiler:Profile
    {
        public ItemProfiler()
        {
            CreateMap<ItemModel, Item>();
            CreateMap<Item, ItemModel>();
        }
    }
}
