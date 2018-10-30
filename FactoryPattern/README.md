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
            Console.WriteLine("Enter a number of wheels between 1 and 12 to build a vehicle and press enter");
           
            var wheels = Console.ReadLine();
            var vehicle = VehicleFactory.Build(Convert.ToInt32(wheels));
            Console.WriteLine($" You built a {vehicle.GetType().Name}");
            Console.Read();
        }
    }
```
If you start the Console Application, you'll be asked a question to enter a number between 1 and 12 and a vehicle will be created depending on the number of wheels supplied.

This is obviously a very trivial example of a Factory Method just in order to put a point across, as to how to actually use them.
If you're looking for a more complex implementation of a Factory Method pattern, take a look at the [source code of my Threenine.Map](https://github.com/threenine/Threenine.Map) application, and more specifically the class I have cunningly named the [MapConfigurationFactory](https://github.com/threenine/Threenine.Map/blob/master/src/MapConfigurationFactory.cs).

You'll notice that this class, is basically the main brain of the application and is responsible for building and loading all the mapping configurations that have been defined within the application.
It has a number of entry points defined, but the main method I always use is the ```Scan``` method, which ultimately is the Abstract Factory Method.

The library itself contains just 3 interfaces ```ICustomMap, IMapFrom, IMapTo``` , which enable developers to implement various mapping logic and associate it with the POCO or Entity classes it relates too. For detailed explanation to how it works [check out the documentation](https://threeninemap.readthedocs.io/en/latest/MapConfigurationFactory.html).

In brief what the ```MapConfigurationFactory``` does, it uses reflection to iterate through all the libraries contained in an assembly and retrieves all the classes have been marked with any of the interfaces and loads the Mappings to the [Automapper - a Convention-based object-object mapper](https://automapper.org/).

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Threenine.Map
{
    public class MapConfigurationFactory
    {
       public static void Scan<TType>(Func<AssemblyName, bool> assemblyFilter = null)
        {
            var target = typeof(TType).Assembly;

            bool LoadAllFilter(AssemblyName x) => true;

            var assembliesToLoad = target.GetReferencedAssemblies()
                .Where(assemblyFilter ?? LoadAllFilter)
                .Select(Assembly.Load)
                .ToList();

            assembliesToLoad.Add(target);

            LoadMapsFromAssemblies(assembliesToLoad.ToArray());
        }

        public static void LoadMapsFromAssemblies(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToArray();
            LoadAllMappings(types);
        }


        public static void LoadAllMappings(IList<Type> types)
        {
            Mapper.Initialize(
                cfg =>
                {
                    LoadStandardMappings(cfg, types);
                    LoadCustomMappings(cfg, types);
                });
        }

        
        public static void LoadCustomMappings(IMapperConfigurationExpression config, IList<Type> types)
        {
            var instancesToMap = (from t in types
                from i in t.GetInterfaces()
                where typeof(ICustomMap).IsAssignableFrom(t) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select (ICustomMap) Activator.CreateInstance(t)).ToArray();


            foreach (var map in instancesToMap)
            {
                map.CustomMap(config);
            }
        }
        
        public static void LoadStandardMappings(IMapperConfigurationExpression config, IList<Type> types)
        {
            var mapsFrom = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select new
                {
                    Source = i.GetGenericArguments()[0],
                    Destination = t
                }).ToArray();


            foreach (var map in mapsFrom)
            {
                config.CreateMap(map.Source, map.Destination);
            }


            var mapsTo = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select new
                {
                    Source = i.GetGenericArguments()[0],
                    Destination = t
                }).ToArray();


            foreach (var map in mapsTo)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }
    }
}

```

###Conclusion
The Factory Method pattern, in my opinion is one of the most important patterns to understand within software development. It's the one pattern that in all likelihood that you'll most often implement. factory design pattern, is used to instantiate objects based on another data type. Factories are often used to reduce code bloat and make it easier to modify which objects need to be created.

 





 
 


  