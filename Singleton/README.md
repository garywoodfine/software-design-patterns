# Singleton Pattern

The Singleton pattern is grouped by the Gang Of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck) as a Creational Pattern, although to some extent it is a pattern that limits, rather than promotes, the creation of classes.

The primary objective of the Singleton Pattern, is to ensure that there is __one and only one__ instance of a class and provides a global access point to it.

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

  // This is not recommended implementation of the Singleton Pattern
  // Just an example of the simplest implementation
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
 
 In the line ` public static Spooler Instance => instance ??= new Spooler();`  We simply make use of the [null-coalescing operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator)  to check if our instance has been instantiated
 and if not create it or return the existing instantiated object.
 
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
                    if (args == null) throw new ArgumentNullException(nameof(args));
        
                    for (int i = 0; i < 12; i++)
                    {
                        Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = $"test-document-{i}"});
                    }
        
                  
                    foreach (var queueItem in Spooler.Instance.Queue)
                    {
                        Console.WriteLine(queueItem.DocumentName);
                    }
                }
            }
```
If we run a simple application we'll see that our application works as expected and there is only one instance of `Spooler` class and we can add values to it no problem. Interesting point to note here, is that because our Singleton class
returns a reference to itself in the instance, we can then just use it in Fluent style i.e. we don't need to create a variable to reference to it.

The problem comes in when we try to use this class in a Multi-Threaded environment. Which I will try to simulate by creating a load of tasks which start a new thread and attempts to add a document to our `Queue` 

### Simple Thread Safe Singleton Implementation
We could improve the above implementation by making use of a `lock`  on the shared object to check if the instance has been created before creating a new one.

Locking ensures that all reads occur logically after the lock is acquired while unlocking ensures all writes occur logically before the lock release. This ensures only one thread can create an instance 

This pattern may address the memory barrier issues, faced with the Simple Implementation, but unfortunately has a performance impact because a lock is acquired every time the instance is requested.

```c#
     public sealed class Spooler : Spool
         {
            private static Spooler instance;
            private static readonly object threadlock = new object();
        
            
            public static Spooler Instance
            {
                get
                {
                    lock (threadlock)
                    {
                        return instance ??= new Spooler();
                    }
                }
            }
        } 

```
###  Double check Lock Singleton Pattern

We could attempt the issues above by implementing a Double check Lock pattern.

This pattern is not recommended and has a number of issues.  

```c#
 public class Spooler : Spool
    {
      
            private static Spooler instance;
            private static readonly object padlock = new object();

            public static Spooler Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (padlock)
                        {
                            if (instance == null)
                            {
                                instance = new Spooler();
                            }
                        }
                    }
                    return instance;
                }
            }
        }
```

### Almost Lazy Singleton Pattern

`static` constructors in C# execute only when an instance of the class is created or a static member is referenced, and to execute only once per AppDomain. The check for the type being newly constructed needs to be executed whatever else happens, it will be faster than adding extra checking as in the previous examples.

This approach is still not ideal and actually has some issues because if you have static members other than Instance, the first reference to those members will involve creating the instance.  There are also additional complications in that  if one static constructor invokes another which invokes the first again.  The laziness of type initializers are only guaranteed by .NET when the type isn't marked with a special flag called `beforefieldinit`. 

This pattern also incurs a performance hit.

```c#
 public class Spooler : Spool
    {
        private static readonly Spooler instance = new Spooler();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Spooler()
        {
        }

        private Spooler()
        {
        }

        public static Spooler Instance => instance;
    }
```
### Full Lazy Implementation

In this pattern instantiation is triggered by the first reference to the static member of the nested class, which only occurs in Instance. 

```c#
   public class Spooler : Spool
    {
        
        private Spooler()
        {
        }

        public static Spooler Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Spooler instance = new Spooler();
        }
        
    }
```

### Generic Lazy Implementation

The .net framework has some really cool features that and `System.Lazy<T>` is one of those features to help provide lazy initialization with access from multiple threads.

The code below implicitly uses `LazyThreadSafetyMode.ExecutionAndPublication` as the thread safety mode for the `Lazy<Spooler>`.

 simpler way to achieve laziness, using .NET 4 +, in my opinion tt also has the advantage that it's obviously lazy and it is clearly and implicitly stated in the code.

```c#
 public class Spooler : Spool
    {
        private static readonly Lazy<Spooler>
            lazy =
                new Lazy<Spooler>
                    (() => new Spooler());

        public static Spooler Instance { get { return lazy.Value; } }

        private Spooler()
        {
        }
        
    }
``` 









 
