using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Models
{
    public class OrderItem
    {
        [key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
