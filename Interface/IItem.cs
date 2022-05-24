using SabaikoOrganicFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Interface
{
    public interface IItem
    {
        List<Item> GetItems();

        Item GetItem(int id);

        Item Create(Item item);

        Item Edit(Item item);

        Item Delete(Item item);

    }
}
