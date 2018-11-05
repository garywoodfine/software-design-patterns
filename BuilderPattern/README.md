# Builder Pattern

The Builder Pattern is a creational Gang of Four (GoF) design pattern,  defined in their seminal book [ Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2N22a2H), in which they presented a catalogue of simple and succinct solutions to commonly occurring design problems.

The pattern is useful for encapsulating and abstracting the creation of objects. It is distinct from the more common [Factory Pattern](https://garywoodfine.com/factory-method-design-pattern/) because the Builder Pattern contains methods of customising the creation of an object.

Whenever an object can be configured in multiple ways across multiple dimensions, the Builder Pattern can simplify the creation of objects and clarify the intent. 

![Builder Pattern](https://garywoodfine.com/wp-content/uploads/2018/11/BuilderPattern.png)

Let's explore the Builder Pattern and how developers can use it to construct objects from components.  You may already seen that the [Factory Pattern](https://garywoodfine.com/factory-method-design-pattern/) returns one of several different subclasses, depending on the data passed in arguments to creation methods.
We'll now learn that the Builder Pattern assembles a number of objects in various ways depending on the data.

### Advantages of the Builder Pattern
* The builder pattern enables developers to hide details of how an object is created
* The builder pattern enables developers to vary the internal representation of an object it builds.
* Each specific builder is independent of others and the rest of the application, improving Modularity and simplifies and enables the addition of other Builders.
* Provides greater control over the creation of objects.

The builder pattern is similar to the Abstract Factory Pattern in that both return classes made up of a number of other methods and objects. 

The main difference between the Builder Pattern and the Abstract Factory Pattern, is that the Abstract Factory Pattern returns a family of related classes and the Builder Pattern constructs a complex object step by step, depending on the data presented to it.