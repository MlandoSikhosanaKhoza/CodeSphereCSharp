using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IItemLogic
    {
        List<Item> GetAllItems();
        void AddItem(Item Item);
        Item GetItem(int ItemId);
        bool UpdateItem(Item Item);
        bool DeleteItem(int ItemId);
    }
}
