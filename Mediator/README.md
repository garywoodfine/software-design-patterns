## The Mediator Pattern


The Mediator Pattern one of the Behavioral Patterns defined by the Gang of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) 

Mediator pattern is used to reduce communication complexity between multiple objects or classes. This pattern provides a mediator class which normally handles all the communications between different classes and supports easy maintenance of the code by loose coupling. 

A mediator sits between method callers and receivers creating a configurable layer of abstraction that determines how callers and receivers get wired up.

### Benefits of the Mediator Pattern
* Increases the reusability of the objects supported by the Mediator by decoupling them from the system.
* Simplifies maintenance of the system by centralizing control logic
* Simplifies and reduces the variety of messages sent between objects in the system.

The Mediator is most commonly used to co-ordinate related GUI components. However, it is also gaining popularity amongst C# developers when developing web API's and Microservices to enable typical Request & Response messaging.

A drawback of the Mediator pattern is that without a proper design the Mediator object itself can become overly complex.

Fortunately, in C# Jimmy Bogard, of [Automapper](https://github.com/AutoMapper/AutoMapper) fame has also developed [MediatR - Simple mediator implementation in .NET](https://github.com/jbogard/MediatR).  MediatR supports  request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.


I prefer to use MediatR as my implementation of choice of the Mediator Pattern, because of its ease of use and versatility. I also learned alot about code by reading the MediatR source code. It is well worth cloning the repository and spending sometime just going through the implementation, it will provide one with a good sense of how to implement the pattern and also an appreciation of some of the finer points of the C# language.


