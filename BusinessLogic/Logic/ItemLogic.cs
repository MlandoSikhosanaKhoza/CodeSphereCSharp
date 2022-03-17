using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ItemLogic:IItemLogic
    {
        private GenericRepository<Item> ItemRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public ItemLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            ItemRepository = UnitOfWork.GetRepository<Item>();
        }

        public List<Item> GetAllItems()
        {
            return (List<Item>)ItemRepository.All();
        }

        public void AddItem(Item Item)
        {
            ItemRepository.Add(Item);
            _unitOfWork.CompleteAsync();
        }

        public Item GetItem(int ItemId)
        {
            return ItemRepository.GetById(ItemId);
        }

        public bool UpdateItem(Item Item)
        {
            ItemRepository.Update(Item);
            _unitOfWork.CompleteAsync();
            return true;
        }

        public bool DeleteItem(int ItemId)
        {
            ItemRepository.Delete(ItemId);
            _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
