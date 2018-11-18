namespace AbstractFactory
{
    public class VehicleFactory : AbstractVehicleFactory
    {
        public override IVehicleFactory CycleFactory()
        {
           return new Cyclefactory(); 
        }

        public override IVehicleFactory MotorVehicleFactory()
        {
            return new MotorVehicleFactory();
        }
    }
}
