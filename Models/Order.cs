using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Models
{
    public class Order
    {
        [key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; }



    }
}
