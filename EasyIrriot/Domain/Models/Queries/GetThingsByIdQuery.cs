namespace Example_EF_1.EasyIrriot.Domain.Models.Queries
{
    public record GetThingsByIdQuery
    {
        public GetThingsByIdQuery(int thingId)
        {
            ThingId = thingId;
        }
        public int ThingId { get; }
    }
}