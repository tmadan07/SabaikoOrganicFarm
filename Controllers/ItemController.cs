using SabaikoOrganicFarm.Interface;
using SabaikoOrganicFarm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItem _itemRepo;
        public ItemController(IItem itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public IActionResult Index()
        {
            List<Item> items = _itemRepo.GetItems();
            return View(items);

        }

        public IActionResult Create()
        {
            Item item = new Item();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            try
            {
                item = _itemRepo.Create(item);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Item itemDetail = _itemRepo.GetItem(id);
            return View(itemDetail);
        }

        public IActionResult Edit(int id)
        {
            Item item = _itemRepo.GetItem(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            try
            {
                item = _itemRepo.Edit(item);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Item item = _itemRepo.GetItem(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Item item)
        {
            try
            {
                item = _itemRepo.Delete(item);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }



    }
}
