# Abstract Factory Pattern

The Abstract Factory Pattern is one level of abstraction higher than [Factory Method Design Pattern](https://garywoodfine.com/factory-method-design-pattern/).  Using the Abstract Factory Pattern a framework is defined, which produces objects that follow a general pattern and at runtime this factory is paired with any concrete factory to produce objects that follow a defined pattern.

The Abstract Factory Pattern is a creational Gang of Four (GoF) design pattern,  defined in their seminal book [ Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2N22a2H), in which they presented a catalogue of simple and succinct solutions to commonly occurring design problems.

### What is the Abstract Factory Pattern
The Abstract Factory Pattern is used when you want to return several related classes of objects, each of which can return several different objects on request. Typically you may use the Abstract Factory Pattern, in-conjunction with other factory patterns like [Simple Factory Pattern](https://garywoodfine.com/simple-factory-pattern/) and the [Factory Method Pattern](https://garywoodfine.com/factory-method-design-pattern/).

The best way to think of the Abstract factory pattern, is that it is a super factory, or a *factory of factories*. Typically it is an interface which is responsible for creating a factory of related objects without explicitly specifying the derived classes.
![Abstract Factory Pattern](https://garywoodfine.com/wp-content/uploads/2018/11/abstractFactoryPattern.jpg)

The key components of the Abstract Factory Pattern are :

**Abstract Factory:** An abstract container which creates abstract objects.

**Concrete Factory:** Implements an abstract container to create concrete object.

**Abstract Object:**  An interface defined with the attributes of the Object to be created.

**Concrete Object:** The actual object by referring related abstract objects.

**Client:**   The client application that creates families of related objects using the factory.

### Example Abstract Factory in C#

In our example we will continue with our sample game application of building a Vehicle depending on a set of user requirements passed into our method.  This is hypothetical game we started creating in the  [Factory Method Design Pattern](https://garywoodfine.com/factory-method-design-pattern/) article. We will *refactor* this application to introduce abstract factory design pattern and provide the user with some additional choices they can provide.

The user will be able to stipulate the Number of Wheels, whether they have an engine, if they will be carrying cargo and if they will be carrying passengers.  Once this information is supplied the application will return the best type of vehicle to suit their purpose. The game will be a simple Console application which simply asks the users to enter their choice and responds with an answer.

I've kept the code rather simple, in order to illustrate a point.  The code for our Entire console is follows:

```c#
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

```

The code in this section despite being quite messy at this point, and pretty hard to read, (This is rather deliberate at this stage because we will be addressing  this  in a future article!) - does nothing more than  ask a user a set of questions and adds answers to a defined `VehicleRequirements` is then passed to method which then calls the `Abstract Factory`  which I will explain shortly.

The `GetVehicle`  Method will return an instance of class which implements our `IVehicle` intefacce.

```c#
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
```
This is the method where we actually start to see the implementation of our Abstract Factory - or our *factory of factories*.  

Our abstract factory is actually an `abstract` class which is defined with 2 methods which require the implementation of 2 additional factories. You'll notice that these two methods are also just implementations of `IVehicleFactory`.

```c#
  public abstract class AbstractVehicleFactory
    {
        public abstract IVehicleFactory CycleFactory();
        public abstract IVehicleFactory MotorVehicleFactory();

    }
```
This class is nothing more than an abstract class and has no implementation code at all. We  could if desired have some default implementation code which could be overidden if desired. However, for my purpose I have just left them as stubs.

The class enables the implementation of two different factories, each will be responsible for returning different vehicle types. In our Case Cycles or Motor Vehicles.
