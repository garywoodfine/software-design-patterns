namespace AbstractFactory
{
    public class VehicleFactory : AbstractVehicleFactory
    {
        private readonly IVehicleFactory _factory;
        private readonly VehicleRequirements _requirements;
        
        public VehicleFactory(VehicleRequirements requirements)
        {
            _factory = requirements.HasEngine  ? (IVehicleFactory) new MotorVehicleFactory() : new Cyclefactory();
            _requirements = requirements;

        }
        public override IVehicle Create()
        {
           return _factory.Create(_requirements);
        }
    }
}
