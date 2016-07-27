using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeWeb.Models
{
    public class Cart
    {
        public List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Item item, int quantity)
        {
            CartLine line = lineCollection
                .Where(i => i.Item.Id == item.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Item = item, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Item item)
        {
            lineCollection.RemoveAll(it => it.Item.Id == item.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(line => line.Item.Price * line.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }


    }

    public class CartLine
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}