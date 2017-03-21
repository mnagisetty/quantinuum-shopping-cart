using System.Collections.Generic;
using ShoppingCartService.EventStore;

namespace ShoppingCartService.ShoppingCart
{
    public class ShoppingCart
    {

        private HashSet<ShoppingCartItem> items = new HashSet<ShoppingCartItem>();
        public int UserId { get; }
        public IEnumerable<ShoppingCartItem> Items { get { return items; } }

        public ShoppingCart(int userId)
        {
            this.UserId = userId;
        }

        public void AddItems(IEnumerable<ShoppingCartItem> items, IEventStore events)
        {
            foreach (var item in items)
            {
                if (this.items.Add(item))
                {
                    events.Raise("ShoppingCart Item Added", new { UserId, item });
                }
            }
        }

        public void RemoveItems(int[] items, IEventStore events)
        {

        }
    }
}