using System;
using System.Globalization;
using AbstractFactory;

namespace VehicleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var requirements = new VehicleRequirements();
          
            Console.WriteLine( "To Build a Vehicle answer the following questions");
            
            Console.WriteLine("How many wheels do you have ");
            var wheels = Console.ReadLine();
            int wheelCount = 0;
            
            if (!int.TryParse(wheels, out wheelCount))
            {
                wheelCount= 1;
            }

            requirements.NumberOfWheels = wheelCount;
            
            Console.WriteLine("Do you have an engine ( Y/n )");
            var engine = Console.ReadLine();
            switch (engine)
            {
                case "Y":
                    requirements.Engine = true;
                    break;
                case "N":
                    requirements.Engine = false;
                    break;
                default:
                    requirements.Engine = false;
                    break;
            }
            
            Console.WriteLine("How many passengers will you be carrying ?  (1 - 10)");

            var passengers = Console.ReadLine();
            var passengerCount = 0;

            if (!int.TryParse(passengers, out passengerCount))
            {
                passengerCount = 1;
            }

            requirements.Passengers = passengerCount;
            
            
            Console.WriteLine("Will you be carrying cargo");

            var cargo = Console.ReadLine();
            switch (cargo)
            {
                case "Y":
                    requirements.Engine = true;
                    break;
                case "N":
                    requirements.Engine = false;
                    break;
                default:
                    requirements.Engine = false;
                    break;
            }

            var vehicle = GetVehicle(requirements);
            
           Console.WriteLine(vehicle.GetType().Name);
        }

        private static IVehicle GetVehicle(VehicleRequirements requirements)
        {
            var factory = new VehicleFactory();
            IVehicle vehicle;


            switch (requirements.NumberOfWheels)
            {
                case 1:
                case 2:
                case 3:
                  vehicle = factory.CycleFactory().Create(requirements);
                  break;
                default:
                    vehicle = factory.MotorVehicleFactory().Create(requirements);
                    break;
            }

            return vehicle;
        }
    }
}
