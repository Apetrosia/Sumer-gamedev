using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    struct InventoryComponent
    {
        public List<Entity> items;

        public override string ToString() // доделать
        {
            return String.Join(" \n", items.Select(e=>e.ToString()));
        }
    }
}