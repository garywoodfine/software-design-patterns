# Strategy Pattern

The *Strategy Pattern* is a *Behavioral Pattern* , [Software Design Patterns](https://garywoodfine.com/software-design-patterns/ "Software Design Patterns | Gary Woodfine") defined by the Gang of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) , it is also discussed in [Head First Design Patterns](https://amzn.to/32YhjIT "Head First Design Patterns"). 

The Strategy pattern enables the definition and encapsulation of a number of algorithms and make them interchangeable. The algorithms may vary independently and may deliver different outcomes and enable different behaviour, however they can all be called using the same method signature.

A typical example of how the Strategy pattern is implemented is illustrated in the diagram below. 

![Stratey Pattern](https://garywoodfine.com/wp-content/uploads/2021/06/image.png)

In Strategy pattern, a class behaviour or its algorithm can be changed at run time. Enabling the creation of objects which represent various strategies and a context object whose behaviour varies as per its strategy object. The strategy object changes the executing algorithm of the context object.

Another name for the Strategy pattern is *Policy*, which in my opinion provides a better clue on how this pattern can be utilised in software applications.  The most common scenario in enterprise software development where the Strategy pattern can be implemented in a Business Rules Engine.

A business rules engine (BRE) is an application that manages decision processes using pre-defined logic to determine outcomes. BREs enable precise decision making, and are especially useful for complex dependencies, as well as in instances where regulatory or organizational rule changes frequently require logic changes.  

An example of this may be in a Bank loan software application, there are various rules that can be triggered based on responses to questions. If the answer is *yes* to one question or *no* to another question, the chances of approval for a loan could be impacted or further subsequent questions may be required to further clarify financial information. These rules help guide users through the application process and help the financial institution determine the eligibility and structure their business.

I recently came across [Basic Rules Engine Design Pattern](https://tenmilesquare.com/basic-rules-engine-design-pattern/ "Basic Rules Engine Design Pattern | ten mile square"), where in the author just actually describes the typical implementation of the Strategy pattern, as you'll be able to tell from the class diagram he supplied, the implementation resembles the simplified Strategy Pattern above.

![Basic Rules Engine](https://garywoodfine.com/wp-content/uploads/2021/06/image-1.png "Basic Rules Engine")

### Implementation

In our very simple implementation of the strategy pattern we'll build a simple console game, in which we'll invite the user to enter a number in the terminal and based on the value we'll return different values. Each of the return values will be returned by different strategies.

let's start by declaring an interface for our strategies, which we'll define as `IRule` which will have two methods a `Verify` and an `Execute`.  
```csharp
 public interface IRule
 {
    bool Verify(string choice);
    void Execute();
 }
```

Essentially the `Verfify` method will taken in the input and determine if the strategy can be executed based on the input and provide a response back to calling application to signify whether it is appropriate to execute the strategy.

The `Execute` will enable the calling application to execute the strategy.

lets implement a first simple strategy class implementing the interface.  We'll implementing a strategy to ensure the user has provided a valid number as input, We'll name our class `NotANumberRule.cs` the code for the class will be as follows.

```csharp
public class NotANumberRule : IRule
 {
    public bool Verify(string choice)
    {
      return !choice.All(char.IsDigit);
    }
    public void Execute()
    {
      Console.WriteLine("We said enter a number ");
    }
 }
```

I've kept the implementation of our class as simple as possible as not to distract from the detail of the strategy pattern.  In the Verify method we iterate through each char in the supplied string to ensure that it is a Numeric digit, if we find any alpha characters then we want t signify that our strategy should be executed because we want to notify the user that they should only enter a number.

We can create any number of Rules classes providing the all implement the `IRule` interface. In the code sample I have implemented several rules each wil trigger on various conditions.

We can now implement our simple Console application, in which we'll prompt the user to enter to choose a number between 1 and 10.

```csharp
 class Program
 {
    static void Main(string[] args)
    {
      string val;
      Console.Write("Enter a number between 1 & 10 : ");
      val = Console.ReadLine();
      
           
 }
```
 We now need to implement one more bit of code, which will basically retrieve all our Rules that we have defined in our application and determine which rule to execute based on the conditions.

We use a some Reflection to retrieve all types in our application and check to see if they implement the `IRule` interface, if they do we will instantiate the class and call the `Verify` method to determine if we should call the `Execute` method

```csharp
 private static void ProcessInput(string input)
 {
    foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
    {
      if (!typeof(IRule).IsAssignableFrom(type) || !type.IsPublic || type.IsInterface) continue;
  
      var rule = (IRule)Assembly.GetExecutingAssembly().CreateInstance(type.FullName, false);
      if (rule.Verify(input)) rule.Execute();
    }
 }
```

With this method now in place we can add it to our Console application.

```csharp
  static void Main(string[] args)
        {
            
            string val;
            Console.Write("Enter a number between 1 & 10 : ");
            val = Console.ReadLine();
            ProcessInput(val);
           
        }
```

