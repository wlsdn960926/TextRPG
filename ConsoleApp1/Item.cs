using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int GoldValue { get; }
        public bool IsEquipped { get; set; }
        public bool IsPurchased { get; set; }

        public Item(string name, string description, int goldValue)
        {
            Name = name;
            Description = description;
            GoldValue = goldValue;
            IsEquipped = false;
            IsPurchased = false;
        }
    }
}
