namespace ShoppingCartService.EventStore
{
    using Nancy;

    public class EventModule : NancyModule
    {
        public EventModule(IEventStore events) : base("events")
        {
            Get("/", _ => 
            {
                long firstSequenceNumber, lastSequenceNumber;

                if(!long.TryParse(this.Request.Query.start.Value, out firstSequenceNumber))
                firstSequenceNumber = 0;

                if(!long.TryParse(this.Request.Query.end.Value, out lastSequenceNumber))
                lastSequenceNumber = long.MaxValue;

                return events.GetEvents(firstSequenceNumber, lastSequenceNumber);
            });
        }
    }
}