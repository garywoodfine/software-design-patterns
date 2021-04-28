## Adapter pattern


The Adapter Design Pattern is the first software design pattern of the Structural Pattern, that the Gang of Four (GOF) Design Patterns,  presented in their book , [Design Patterns - Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck)
that I will discuss in series of Structural Design Patterns.

The Adapter design pattern is a structural pattern that allows incompatible interfaces, from disparate systems to exchange data and work together.  It is extremely useful when integrating tool-kits, libraries and other utilities together.  Apple Macbook users will be very familiar adapters, which they will frequently use to plug various devices and network connections etc.

[![USB C Hub Multiport Adapter, 13-in-1 MacBook Pro USB Type C Adapter Hub with Ethernet, VGA, 4K HDMI, 3 USB 2.0, 2 USB 3.0, 3-Slot Card Reader, 3.5mm Audio and Power Delivery](https://garywoodfine.com/wp-content/uploads/2021/04/adapter-pattern.jpg)](https://amzn.to/3gHAkXX)

This is essentially a physical implementation of the Adapter pattern.  [Head First Design Patterns](https://amzn.to/3tSqSVo "Head First Design Patterns: Building Extensible and Maintainable Object-Oriented Software")  provides a really good succinct definition of the Adapter Pattern.

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

