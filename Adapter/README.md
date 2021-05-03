## Adapter pattern

The Adapter Design Pattern is the first software design pattern of the Structural Pattern, that the Gang of Four (GOF) Design Patterns,  presented in their book , [Design Patterns - Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck)
that I will discuss in series of Structural Design Patterns.

The Adapter design pattern is a structural pattern that allows incompatible interfaces, from disparate systems to exchange data and work together.  It is extremely useful when integrating tool-kits, libraries and other utilities together.  Apple Macbook users will be very familiar adapters, which they will frequently use to plug various devices and network connections etc.

[![USB C Hub Multiport Adapter, 13-in-1 MacBook Pro USB Type C Adapter Hub with Ethernet, VGA, 4K HDMI, 3 USB 2.0, 2 USB 3.0, 3-Slot Card Reader, 3.5mm Audio and Power Delivery](https://garywoodfine.com/wp-content/uploads/2021/04/adapter-pattern.jpg)](https://amzn.to/3gHAkXX)

This is essentially a physical implementation of the Adapter pattern.  [Head First Design Patterns](https://amzn.to/3tSqSVo "Head First Design Patterns: Building Extensible and Maintainable Object-Oriented Software")  provides a really good succinct definition of the Adapter Pattern.

> The Adapter pattern allows you to provide an object instance to a client that has a dependency on an interface that your instance does not implement. An Adapter class is created that fulfils the expected interface of the client but that implements the methods of the interface by delegating to different methods of another object.
> 
> [Adaptive Code: Agile coding with design patterns and SOLID principles]()

### Two types of Adapters.

There are typically two kinds of of adapters:
* *object* adapters
* *class* adapters

#### Class adapters
Typically you may only really encounter this type of pattern when using C or C++ or other languages that enable multiple inheritance.

#### Object adapters
It basically the only adapter pattern C# developers have available.

> **The Adapter Pattern**
> 
>  Converts the interface of a class into another interface this client expects. Adapter lets classes work together that couldn't otherwise because of incompatible interfaces.

The main advantage of this pattern is that it enables the use of another library in an application that has an incompatible interface by using an adapter that does the conversion. 


![Adapter Pattern](https://garywoodfine.com/wp-content/uploads/2021/04/adapter-pattern.png)

### When to use the Adapter Pattern?
There are a number of situations when making use of the Adapter pattern can be a great solution

* A class needs to be reused that does not have an interface that a client requires.
* Allow a system to use classes of another system that is incompatible with it.
* Allow communication between a new and already existing system that is independent of each other.
* Sometimes a toolkit or class library cannot be used because its interface is incompatible with the interface required by an application.

### Simple Adapter pattern implementation
In its most simple form the Adapter Pattern can just be a simple *wrapper* class which implements an interface. However the implementation within the class my implement a different set of classes to deliver the functionality required.

You may have an interface for Transport class that defines a `Commute` method.
```c#
 public interface ITransport
 {
    void Commute();
 }
```

However, the only class you have a available is `Bicycle` class that has a `Pedal` method which will work for what you need.

```c#
 public class Bicycle
    {
      public void Pedal()
        {
            Console.WriteLine("Pedaling");
        }
    }
```

The snag is that the method or class that is going to use it, can only use classes that implement the `ITransport` interface.  

We can use the Adapter pattern here to create a class that implements the `ITransport` interface, but it actually just wraps the `Bicycle` class.

```c#
 public class Transport : ITransport
    {
        private Bicycle _bike => new Bicycle();
        public void Commute()
        {
           _bike.Pedal();
        }
    }
```

You can now implement the `Transport` class in your application, because it implements the `ITransport` interface.

```c#
 class Program
 {
    static void Main(string[] args)
    {
       var transport = new Transport();
       transport.Commute();
    }
 }
```
That is all there is to the Adapter pattern. Even in this most simplistic of implementations you can see the power of enabling classes that my seem incompatible with your application , yet you can still make use of them.

### Conclusion 
The Adapter Pattern is a very simple pattern, but can be quite powerful and extremely useful, and is really a worthwhile pattern to be aware of. Many developers, will most likely have used the Adapter pattern, without actually being explicitly aware of it.

The are more advanced implementation details of the Adapter pattern, but the fundamentals of the pattern remain the same.