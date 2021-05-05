## The Mediator Pattern


The Mediator Pattern one of the *Behavioral Patterns* defined by the Gang of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) 

Mediator pattern is used to reduce communication complexity between multiple objects or classes. The pattern provides a mediator class which handles all the communications between different classes and supports easy maintenance of the code by loose coupling. 

A mediator sits between method callers and receivers creating a configurable layer of abstraction that determines how callers and receivers get wired up.

### Benefits of the Mediator Pattern
* Increases the reusability of the objects supported by the Mediator by decoupling them from the system.
* Simplifies maintenance of the system by centralizing control logic
* Simplifies and reduces the variety of messages sent between objects in the system.

The Mediator is most commonly used to co-ordinate related GUI components. However, it is also gaining popularity amongst C# developers when developing web API's and Microservices to enable typical Request & Response messaging.

A drawback of the Mediator pattern is that without a proper design the Mediator object itself can become overly complex.

Fortunately, in C# Jimmy Bogard, of [Automapper](https://github.com/AutoMapper/AutoMapper) fame has also developed [MediatR - Simple mediator implementation in .NET](https://github.com/jbogard/MediatR).  MediatR supports  request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.


I prefer to use MediatR as my implementation of choice of the Mediator Pattern, because of its ease of use and versatility. I also learned alot about code by reading the MediatR source code. It is well worth cloning the repository and spending sometime just going through the implementation, it will provide one with a good sense of how to implement the pattern and also an appreciation of some of the finer points of the C# language.

###  Why use the Mediator pattern
When working with Domain Classes where multiple functions are used to modify state and implement domain rules it usually becomes difficult to debug, extend and review the implementation.  Often business rule functions implement multiple sub-rules that are repeatedly required elsewhere in the domain. This may lead to Multiple layers of complex abstraction required to share functionality or multiple strategies from several developers advocating different patterns and practices.

A common challenge in a codebase, is dealing with the constant flux of ever changing domain requirements and business rules.

Typically software services start out as simple CRUD applications, but gradually evolve to become complex as more rules and changes are introduced.

Developers may initially try to solve this problem, by implementing a *Manager* class to centralise the logic to contain every action possible to modify the state of an entity or to modify the state of another *manager* class.

Testing often becomes convoluted, messy, complex and usually incomplete. Typically the problems occur when in order to test the class there are a number of dependencies to mock and configure. Just to test a new function.

Typically no validation of the inbound request is implemented, and the request is allowed to pass through the middle layer, API layer and only validated within the Manager class functions. This often causes bloated action functions with complicated validation rules before the implementation logic!

### How does the Mediator pattern solve issues

The mediator pattern promotes loose coupling, by implementing a mediator object to enable objects to communicate with it rather than each other.

## Example of the mediator pattern

In [Developing Api’s using Http Endpoints](https://garywoodfine.com/developing-apis-using-http-endpoints/) I discussed some of the problems relating to using MVC pattern to developing Web API projects and how to overcome by making use of the [Adralis API Endpoints](https://github.com/ardalis/ApiEndpoints) and I even created a [API Template project](https://garywoodfine.com/how-to-create-project-templates-in-net-core/) to help you get started. In this example we are going to make use of the template to create a project to illustrate how to use the Mediator Pattern, and we will also be making use of MediatR, because the template project comes pre-configured to use it.

Once you have [installed the template](https://www.nuget.org/packages/threenine.ApiProject/)  we can create a new project using

```c#
dotnet new apiproject -n mediator
```
Once the project has been generated, we will have have all that is required done for us to provide the most simplistic example of the Mediator pattern.

It is important that the basis of the Mediator pattern, is Request & Response mediation. 

> Mediator promotes loose coupling by keeping objects from referring to each other explicitly and it lets you vary their interaction independently
> 
>  [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) 

![](https://garywoodfine.com/wp-content/uploads/2021/05/mediator.png)
