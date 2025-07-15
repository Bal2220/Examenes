using Example_EF_1.Shared.Domain.Models.Entities;

namespace Example_EF_1.EasyIrriot.Domain.Models.Entities
{
    public class Thing : BaseEntity
    {
        public Thing() {}

        public Thing(string model, decimal maximumTemperatureThreshold, decimal minimumHumidityThreshold)
        {
            SerialNumber = Guid.NewGuid().ToString();
            OperationMode = EOperationMode.ScheduleDriven;
            Model = model;
            MaximumTemperatureThreshold = maximumTemperatureThreshold;
            MinimumHumidityThreshold = minimumHumidityThreshold;
            CreatedAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public EOperationMode OperationMode { get; set; }
        public decimal MaximumTemperatureThreshold { get; set; }
        public decimal MinimumHumidityThreshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<ThingState> ThingStates { get; set; }
    }
}