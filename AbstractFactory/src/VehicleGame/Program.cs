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
            switch (engine.ToLower())
            {
                case "y":
                    requirements.HasEngine = true;
                    break;
                case "n":
                    requirements.HasEngine = false;
                    break;
                default:
                    requirements.HasEngine = false;
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
            switch (cargo.ToLower())
            {
                case "y":
                    requirements.HasEngine = true;
                    break;
                case "n":
                    requirements.HasEngine = false;
                    break;
                default:
                    requirements.HasEngine = false;
                    break;
            }

            var vehicle = GetVehicle(requirements);
            
           Console.WriteLine(vehicle.GetType().Name);
        }

        private static IVehicle GetVehicle(VehicleRequirements requirements)
        {
            var factory = new VehicleFactory();
            IVehicle vehicle;

            if (requirements.HasEngine)
            {
                return factory.MotorVehicleFactory().Create(requirements);
            }
 
           return factory
                .CycleFactory().Create(requirements);

         
        }
    }
}
