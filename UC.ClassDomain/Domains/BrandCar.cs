

namespace UC.ClassDomain.Domains
{
    public class BrandCar
    {
        public int BrandCar_ID { get; set; }
        public string BrandCar_Name { get; set; }
        public VehicleType BrandCar_Type { get; set; }
    }

    public enum VehicleType
    {
        Sedan = 0,
        Pickup = 1,
        Van = 2,
        Truck = 3,
        PickupTruck = 4,
        Bus = 5,
        Minibus = 6,
        Motorcycle = 7,
    }
}
