using System;

namespace ShoppingCartService.EventStore
{
    public class Event
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Name { get; set; }
        public object Content { get; set; }
    }
}