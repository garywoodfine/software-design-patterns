#Factory Method Design Pattern

The factory concept is probably the most common design patterns and recurs throughout the object-oriented programming.
You will find countless references and uses of the factory pattern within the [.net core foundational libraries](https://github.com/dotnet/corefx) and throughout the .net framework source code, most notable and probably one of the most commonly used factories can be found in the `System.Data.Common` namespace  and the `DbProviderFactories`.

The factory method design pattern falls under the Creational Design Patterns of  [Gang of Four (GOF) Design Patterns](https://amzn.to/2RxkPTm), and is most commonly used to create objects.

##What is the Factory Method Pattern

The factory method pattern is a clever but subtle extension to the concept that a class acts as a traffic cop and decides which sub class of a single hierarchy will be instantiated.

In the factory pattern, developers create an object without exposing the creation logic. An interface is used for creating an object, but lets subclass decide which class to instantiate. Rather than defining each object manually, developers can do it programmatically.

In short, a factory is an object that creates objects without the use of a constructor. 

The pattern does not actually have a decision point where one subclass is directly selected over another class. Instead, programs written to this pattern usually define an abstract class that creates objects but lets each subclass decide which object to create.



##When to use a Factory Method
 The are a number of circumstances when developing an application when making use of the Factory Method is suitable. These situation include :
 * A class can't anticipate which class objects it must create
 * A class uses its subclasses to specify which objects it creates
 * You need to localize the knowledge of which class gets created
 
It is generally considered a bad idea for base classes to know implementation details of their derived types.  It is in situations like this is when you should use the Abstract Factory pattern.

A Typical situation maybe  when a constructor needs to return an instance of type within which it resides, a factory method is able to return many different types of objects, all which belong to the same inheritance hierarchy.

##How to implement Factory Pattern
We'll develop a factory method that enables the creation of a vehicle depending on the number of wheels required

![factory pattern](https://garywoodfine.com/wp-content/uploads/2018/10/factory-pattern.jpg)

A vehicles can consist of any number of wheels,  as an example, in a game when character needs to build a vehicle when they have set number of wheels.

If we pass the number of Wheels to our factory method it will build the vehicle and return it.

We'll keep this example trivial so we don't get lost in the detail, so we will define a simple interface of ```IVEHICLE``` which is nothing more than empty interface.
We'll also Define 4 classes which will Inherit the ```IVehicle``` interface. Unicycle, MotorBike, Car and Truck

```c#
 public interface IVehicle
 {
       
 }
 public class Unicycle : IVehicle
 {
             
 }
 public class Car : IVehicle
 {
            
 }
 public class Motorbike : IVehicle
 {
                
 }
 public class Truck : IVehicle
 {
          
 }        
    
```

We can now create a ```VechicleFactory``` class with a build method to create a vehicle depending on the
 number of wheels supplied.
 
 ```c#
 public static class VehicleFactory
    {
        public static IVehicle Build(int numberOfWheels)
        {
            switch (numberOfWheels)
            {
                case 1:
                    return new UniCycle();
                case 2:
                case 3:
                    return new Motorbike();
                case 4:
                    return new Car();
                default :
                    return new Truck();
            
            }
        }
    }
```

We can now develop our little game, in this example it will be a Console Application, and we'll ask the user to enter a number of 
wheels and then we'll tell them what type of Vehicle they built.

```c#
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
```





 
 


  