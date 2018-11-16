namespace AbstractFactory
{
    public class BicycleFactory : IVehicleFactory
    {
        public IVehicle Create(VehicleRequirements requirements)
        {
            switch (requirements.Passengers)
            {
                case 1:
                    if(requirements.NumberOfWheels == 1) return new Unicycle();
                    return new Bicycle();
                case 2:
                    return new Tandem();
                default:
                    return new Bicycle();
            }
        }
    }
}
