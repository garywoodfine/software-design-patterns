using System;

namespace AbstractFactory
{
    public interface IVehicleFactory
    {
        IVehicle Create(VehicleRequirements requirements);
    }
}
