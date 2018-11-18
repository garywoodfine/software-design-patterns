namespace AbstractFactory
{
    public class Cyclefactory : IVehicleFactory
    {
        public IVehicle Create(VehicleRequirements requirements)
        {
          return new BicycleFactory().Create(requirements);
       }
    }
}
