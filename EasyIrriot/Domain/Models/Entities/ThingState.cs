using Example_EF_1.Shared.Domain.Models.Entities;

namespace Example_EF_1.EasyIrriot.Domain.Models.Entities
{
    public class ThingState : BaseEntity
    {
        public ThingState() {}

        public ThingState(Guid thingSerialNumber, int currentOperationMode, decimal currentTemperature, decimal currentHumidity, DateTime collectedAt)
        {
            ThingSerialNumber = thingSerialNumber.ToString();
            CurrentOperationMode = currentOperationMode;
            CurrentTemperature = currentTemperature;
            CurrentHumidity = currentHumidity;
            CollectedAt = collectedAt;
            CreatedAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string ThingSerialNumber { get; set; }
        public int CurrentOperationMode { get; set; }
        public decimal CurrentTemperature { get; set; }
        public decimal CurrentHumidity { get; set; }
        public DateTime CollectedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Thing Thing { get; set; }
        public int ThingId { get; set; }
    }
}