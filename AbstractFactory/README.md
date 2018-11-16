# Abstract Factory Pattern

The Abstract Factory Pattern is one level of abstraction higher than [Factory Pattern](https://garywoodfine.com/factory-method-design-pattern/).  Using the Abstract Factory Pattern a framework is defined, which produces objects that follow a general pattern and at runtime this factory is paired with any concrete factory to produce objects that follow a defined pattern.

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



