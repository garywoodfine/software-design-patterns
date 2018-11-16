using System;

namespace AbstractFactory
{
    public interface IVehicleFactory
    {
        IVehicle Build(int wheels, bool engine = false);
    }
}
