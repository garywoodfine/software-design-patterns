namespace AbstractFactory
{
    public class Cyclefactory : IVehicleFactory
    {
        public IVehicle Create(VehicleRequirements requirements)
        {
         
            return requirements.Engine ? new MotorBikeFactory().Create(requirements) : new BicycleFactory().Create(requirements);
       }
    }
}
