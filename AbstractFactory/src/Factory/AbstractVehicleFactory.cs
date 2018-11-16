namespace AbstractFactory
{
    public abstract class AbstractVehicleFactory
    {
        public abstract IVehicleFactory CycleFactory();
        public abstract IVehicleFactory MotorVehicleFactory();

    }
}
