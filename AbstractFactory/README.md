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

            if (requirements.HasEngine)
            {
                return factory.MotorVehicleFactory().Create(requirements);
            }
 
           return factory
                .CycleFactory().Create(requirements);
        
        }
```
This is the method where we actually start to see the implementation of our Abstract Factory - or our *factory of factories*.  

In this contrived example, our method is going to do a check to see, if the type of vehicle we want to build has an engine or not and based on that data it will select whether we want to create a Motor powered vehicle or Pedal powered vehicle and redirect to appropriate factory to create the vehicle.  Ideally, you'd probably defer this choice the Abstract factory itself, but I just wanted to illustrate the point, that using an abstract factory class we can still make decisions on which factory we wanted to use. 

Both of the methods defined on the abstract class will provide us with an object implementing `IVehicle` interface.  The application doesn't control the ultimate end type of object but it can select which factory it wants to build the object based on certain criteria.

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

Our actual implementation of our class abstract factory class, would possibly look something like the following.

```c#
 public class VehicleFactory : AbstractVehicleFactory
    {
        public override IVehicleFactory CycleFactory()
        {
           return new Cyclefactory(); 
        }

        public override IVehicleFactory MotorVehicleFactory()
        {
            return new MotorVehicleFactory();
        }
    }
```
Our implementation  of our Vehicle Factory class, in truth is probably  considered bad coding practice,  are nothing more than **Pass Through Methods**  , but I have intentionally implemented this to illustrate a point that ultimately our methods will more than likely invoke an alternate factory. 

> A Pass Through Method is one that does little except invoke another method, whose signature is similar or identical to that of the calling method.
>This typically denotes there is not a clean division between classes
>
>--- [John Ousterhout - A Philosophy of Software Design](https://garywoodfine.com/philosophy-of-software-design/) 

If we take a look at an implementation of one of the factories  i.e `CycleFactory` you'll notice that I have actually elected to make use of the [Factory Method Pattern](https://garywoodfine.com/factory-method-design-pattern/) to create an instance of an object to return.  I chose this to labour another point, in that as a developer you can use these patterns interchangeably and coincide with other patterns.

```c#
 public class Cyclefactory : IVehicleFactory
    {
        public IVehicle Create(VehicleRequirements requirements)
        {
            switch (requirements.Passengers)
            {
                case 1:
                    if(requirements.NumberOfWheels == 1) return new Unicycle();
                    return new Bicycle();
                case 2:
                    return new Tandem();
                case 3:
                    return new Tricyle();
                case 4:
                    if (requirements.HasCargo) return new GoKart();
                    return new FamilyBike();
                default:
                    return new Bicycle();
            }
       }
    }
```
This also serves as an emphasis on the notion of **Factory of Factories** concept. 

I have tried to keep the logic in this sample as contrived as possible, but also try to illustrate that this factory is ultimately responsible for instantiating the object that is passed back to calling client.  This serves to highlight that, the client does not need to concern itself with creating the object and that the decision point for which object to return is separated by several layers.

This enables improved testability via separation of concerns.  Even in this simple example you would notice that client, only needs to concern itself with data that has been entered i.e. Validate the data as close the source as possible, it then passes that data to an abstract factory class which then disseminates it to the required factories to create an object.

It is this feature which makes the Abstract Factory method such a popular and powerful pattern to learn to provide greater opportunity to unit test code.  Using the pattern in-conjunction with other patterns further enhance testability of your code.

### Refactor the Factory

The code for our abstract factory is working, but lets be honest it is rather **Dirty Code** and it does not actually provide all the benefits we could get from using the abstract factory pattern. For a few reasons I alluded to earlier. 

Let's conduct a slight refactor of factory pattern.  The first one, is we could probably do with either removing the option of the client app from having to make the decision of which factory to use, or we could extend on that option. 

In order to illustrate an option, lets do a complete refactor and tidy up the code.  My first option would be to remove those nasty **Pass Through Methods** they really do not provide a clean abstraction and in actually fact we could remove the the need to have 2 Factory methods on our class and just provide one.  This will make our class even easier to understand and remove any chance of errors.

In our case lets just provide the client with one choice of `Factory`.  Our new refactored abstract factory class will now only have one method.

```c#
    public abstract class AbstractVehicleFactory
       {
           public abstract IVehicle Create();
       }
```

With this complete, lets go ahead and complete the refactor of the implementation of our Factory Class.
```c#
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

```

You'll notice here, we've actually used the constructor to pass in the requirements and use the data to determine which factory is best suited to create our vehicle of choice.  This will minimize the risk of the client using the wrong factory.

The complexity of clients `GetVehicle` method has greatly been reduced, they now no longer need to concern themselves with inspecting the data and determining which factory to use.  All they need to do is pass in the `VehicleRequirements` in the constructor and call the `Create` method and all the magic will just happen.

```c#
 private static IVehicle GetVehicle(VehicleRequirements requirements)
        {
            var factory = new VehicleFactory(requirements);
            return factory.Create();
        }
```

What we have achieved here, is an important aspect of *Abstractions* 

> An Abstraction is a simplified view of an entity, which omits unimportant details.
>
>--- [John Ousterhout - A Philosophy of Software Design](https://garywoodfine.com/philosophy-of-software-design/) 

We have managed to [Pull Complexity Downwards](https://garywoodfine.com/philosophy-of-software-design#complexity) and shielded our users from complexity providing them with an easy to use interface.

> When developing a module, look for opportunities to take a little bit of extra suffering upon yourself in order to reduce the suffering of your users.
>
>--- [John Ousterhout - A Philosophy of Software Design](https://garywoodfine.com/philosophy-of-software-design/) 



### Conclusion
The Abstract Factory pattern enables developers to generically define families of related objects, leaving the actual concrete implementation of those objects to be implemented as needed.

 The Abstract Factory pattern is pretty common in C#, and the .net Framework has libraries that use it to provide a way to extend and customize their standard components.
 
 ## Sponsored by 
 [![threenine logo](http://static.threenine.co.uk/img/github_footer.png)](https://threenine.co.uk/)
 
 



