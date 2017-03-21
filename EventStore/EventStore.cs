using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShoppingCartService.EventStore
{
    public class EventStore : IEventStore
    {

        private static long currentSequenceNumber = 0;
        private static readonly IList<Event> database = new List<Event>();

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber) => database
          .Where(e =>
            e.Id >= firstEventSequenceNumber &&
            e.Id <= lastEventSequenceNumber)
          .OrderBy(e => e.Id);

        public void Raise(string eventName, object content)
        {

            //TODO: implement with a real database provider.
            
            var seqNumber = Interlocked.Increment(ref currentSequenceNumber);

            database.Add(new Event
            {
                Key = Guid.NewGuid(),
                Timestamp = DateTimeOffset.UtcNow,
                Name = eventName,
                Content = content
            });
        }
    }
}