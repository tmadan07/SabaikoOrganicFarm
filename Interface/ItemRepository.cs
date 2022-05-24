using SabaikoOrganicFarm.Data;
using SabaikoOrganicFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Interface
{
    public class ItemRepository : IItem
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ItemRepository()
        {

        }

        public Item Create(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return item;

        }

        public Item Delete(Item item)
        {
            _db.Items.Attach(item);
            _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
            return item;

        }

        public Item Edit(Item item)
        {
            _db.Items.Attach(item);
            _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return item;

        }

        public Item GetItem(int id)
        {
            Item itemDetails = _db.Items.Where(item => item.Id == id).FirstOrDefault();
            
            return itemDetails;

        }

        public List<Item> GetItems()
        {
            List<Item> items = _db.Items.ToList();
            return items;

        }
    }

}
