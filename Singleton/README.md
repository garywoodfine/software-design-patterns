# Singleton Pattern

The Singleton pattern is grouped by the Gang Of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck) as a Creational Pattern, although to some extent it is a pattern that limits, rather than promotes, the creation of classes.

The primary objective of the Singleton Pattern, is to ensure that there is one and only one instance of a class and provides a global access point to it.

There are a number of instances in software development where one will need to ensure that there is only one instance of a class. One such example, which may be typical for enterprise software developers is to ensure there is a single point of access to a database engine.

The Singleton Pattern creates a single class responsible to create an object ensuring that only a single object gets created. This class provides a way to access its only object which can be accessed directly without the need to instantiate the object of the class.

> The singleton pattern is a design pattern that restricts the instantiation of a class to one object.

The Singleton Pattern does not allow any parameters to be specified when creating the instance - as otherwise a second request for an instance but with a different parameter could be problematic!

If the same instance should be accessed for all requests with the same parameter, the [Abstract factory pattern](https://garywoodfine.com/abstract-factory-design-pattern/) is more appropriate.

### Creational Design Patterns
* [Simple Factory Pattern ](https://garywoodfine.com/simple-factory-pattern/)
* [Factory Method Pattern](https://garywoodfine.com/factory-method-design-pattern/)
* [Abstract Factory Pattern](https://garywoodfine.com/abstract-factory-design-pattern/)
* [Builder Pattern](https://garywoodfine.com/the-builder-pattern-net-core/)
* [Prototype Pattern](https://garywoodfine.com/the-prototype-design-pattern-c-net-core/)


## Implementing the Singleton Pattern

There are a number of different ways of implementing the Singleton Pattern in C#, each suited to different situations and requirements, which include Simple, Thread Safe, Lazy Loading and High Performance.

### Common Characteristics of Singleton Pattern Implementations.

1. Single Constructor - Private and Parameter-less.
2. Sealed Class i.e. Cannot be inherited
3. Static variable references the single created instance
4. Public static access to the single created instance

### Singleton Pattern Implementation

In our fictional implementation of the Singleton Pattern we will be using it to create Print Spooler class.  

The print spooler is a software service that manages the printing process. The spooler accepts print jobs from the computer and makes sure that printer resources are available. It also schedules the order in which jobs are sent to the print queue for printing.
 
 In the early days of personal computers, you had to wait until a document printed before you could do anything else. Thanks to modern print spoolers, the printing process has minimal impact on user productivity.
 
 
## Simple Singleton Pattern 

The simplest implementation of the Singleton Pattern is not thread safe, which may result in two different threads evaluate the instance value to `null` and actually create two instances of the object thus completely violating the core concept of the Singleton Pattern.

```c#
  public sealed class Spooler
   {
       private static Spooler instance = null;
   
       private Spooler()
       {
           
       }
   
       public static Spooler Instance => instance ??= new Spooler();
   }
```
 In the above example, it is possible for the instance to be created before the expression is evaluated, but the memory model doesn't guarantee that the new value of instance will be seen by other threads unless suitable memory barriers have been passed. 
 
 In a simple single thread application model the example will work, but this is not an optimal implementation.
 
 In the following, example we'll create an `abstract` class which our Singleton class will extend, to basically add some features we can reuse throughout our further examples.
 
 We'll add a Spool class which just preforms a very rudimentary implementation of a print queue which will serve for a demo purposes.
 
 ```c#
  public abstract class Spool
  {
    public List<PrintQueueItem> Queue { get; } = new List<PrintQueueItem>();
  }
```

We will then simply inherit this class in our Singleton class

```c#
 public sealed class Spooler : Spool
   {
       private static Spooler instance = null;
   
       private Spooler()
       {
           
       }
   
       public static Spooler Instance => instance ??= new Spooler();
       
   }
```

We'll also create a really simple console application to illustrate how we would implement and use this Singleton Pattern Implementation.

```c#
    class Program
        {
            static void Main(string[] args)
            {
                int i = 1;
                // We can just access the instance of the class then we can just access the methods 
                // Available on the instance.
                Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = $"Test{i}.docx"});
                Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = $"Test{i + 1}.docx"});
                
                    foreach (var doc in Spooler.Instance.Queue)
                    {
                        Console.WriteLine(doc.DocumentName);
                    }
               
            }
        }
```
If we run a simple application we'll see that our application works as expected and there is only one instance of `Spooler` class and we can add values to it no problem.

The problem comes in when we try to use this class in a Multi-Threaded environment. Which I will try to simulate by creating a load of tasks which start a new thread and attempts to add a document to our `Queue` 

```c#
     static void Main(string[] args)
     {
            for (var i = 0; i < 5; i++)
            {
                var spool = Task.Factory.StartNew(() =>
                    Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"Test{i}.docx"}));
            }
           
            foreach (var doc in Spooler.Instance.Queue)
            {
                Console.WriteLine(doc.DocumentName);
            }
     }

```

If we run this code you'll see nothing actually gets added to the queue and nothing actually is printed out!








 
