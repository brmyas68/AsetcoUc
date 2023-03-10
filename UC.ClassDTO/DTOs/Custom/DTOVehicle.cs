using UC.ClassDomain.Domains;

namespace UC.ClassDTO.DTOs.Custom
{
    public class DtoVehicle
    {
        public DtoVehicle()
        {
            Models = new List<DtoModelCar>();
        }

        public int BrdCar_ID { get; set; }
        public string BrdCar_Name { get; set; }
        public VehicleType BrdCar_Typ { get; set; }
        public List<DtoModelCar> Models { get; set; }
    }
}
