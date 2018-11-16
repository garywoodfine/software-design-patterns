using System.Diagnostics;

namespace AbstractFactory
{
    public class MotorCarFactory : IVehicleFactory
    {
        public IVehicle Create(VehicleRequirements requirements)
        {
            if (!requirements.Cargo && requirements.NumberOfWheels == 4)
            {
                if (requirements.Passengers >= 4)
                {
                    return new Sedan();
                    
                }
                else
                {
                    return new MPV();
                }
                
            }

            if (requirements.Cargo && requirements.NumberOfWheels == 4)
            {
                return new Van();
            }

            switch (requirements.NumberOfWheels)
            {
                case 6:
                case 8:
                    return new Truck();
                case 10:
                case 12 :
                    return new HeavyGoodsTruck();
               default :
                   return new ArticulatedLorry();
                    
            }
            
        }
    }
}
