using System;
using FactoryPattern;

namespace VehicleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number of wheels between 2 and 12 to build a vehicle and press enter");
           
            var wheels = Console.ReadLine();
            var vehicle = VehicleFactory.Build(Convert.ToInt32(wheels));
            Console.WriteLine($" You built a {vehicle.GetType().Name}");
            Console.Read();
        }
    }
}
