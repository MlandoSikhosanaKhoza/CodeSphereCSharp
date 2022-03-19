using AutoMapper;
using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class ItemController : Controller
    {
        public IItemLogic _itemLogic { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public IMapper Mapper { get; set; }
        public ItemController(IItemLogic itemLogic,IMapper mapper,IHostingEnvironment hostingEnvironment)
        {
            _itemLogic = itemLogic;
            Mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult Index()
        {
            return View(_itemLogic.GetAllItems());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = _itemLogic.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemModel item)
        {
            if (ModelState.IsValid)
            {
                string extension = item.ImageName.Split(".")[1];
                string imageUrl;
                item.ImageName = $"{Guid.NewGuid().ToString()}.{extension}";
                byte[] image = Convert.FromBase64String(item.Base64);
                imageUrl = $"{_hostingEnvironment.WebRootPath}/image/item-image/{item.ImageName}";
                System.IO.File.WriteAllBytes( imageUrl, image);
                _itemLogic.AddItem(Mapper.Map<Item>(item));
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = _itemLogic.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }
            return View(Mapper.Map<ItemModel>(item));
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemModel item)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(item.Base64))
                {
                    byte[] image = Convert.FromBase64String(item.Base64);
                    string imageUrl = $"{_hostingEnvironment.WebRootPath}/image/item-image/{item.ImageName}";
                    System.IO.File.WriteAllBytes(imageUrl, image);
                }
                _itemLogic.UpdateItem(Mapper.Map<Item>(item));
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = _itemLogic.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _itemLogic.DeleteItem(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
