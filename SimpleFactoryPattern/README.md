
# Simple Factory Pattern

The post [Simple Factory Pattern](https://garywoodfine.com/simple-factory-pattern/) appeared first on [Gary Woodfine](https://garywoodfine.com) and is also available on [Dev.to](https://dev.to/gary_woodfine/simple-factory-pattern-in-c-and-net-core-3263).

In software development, a Software Design Pattern is a reusable solution to commonly recurring problems. A software design pattern is a description or template used to solve a problem that can be used in many different situations.

In 1994, the so-called Gang Of Four (GOF) published their book,[ Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2N22a2H), in which they presented a catalogue of simple and succinct solutions to commonly occurring design problems.

The book captured 23 Patterns that enabled software architects to create flexible, elegant and ultimately reusable design patterns without having to rediscover or reinvent the design solutions for themselves.  It's a great resource, for those developers who want to learn more about common software design patterns and how to implement them. However, the book doesn't cover all software design patterns, and it definitely doesn't cover some of the most frequently found software patterns.
	
The Simple Factory Pattern is probably one of the most widely used patterns and at the same time, it is also one of the most underused software patterns. I frequently come across scenarios in code bases, when developers have encountered a problem and instead of elegantly handling it, they often pollute function methods with additional lines of cruft logic. Which can often be the source of additional logic bugs or lead to scalability and adaptability issues later.
	
Simple Factory Pattern is a Factory class in its simplest form, compared to Factory Method Pattern or Abstract Factory Pattern, is a <em>factory object for creating other objects</em>. In simplest terms Factory helps to keep all object creation in one place and avoid of spreading <code>new</code> key value across the codebase.
	
The classes a Simple Factory Pattern returns will have the same parent class and methods but will perform the task differently dependent on the type of data supplied.

## Simple Factory Pattern Implementation

Let's take a look at the diagram to get a high-level view of how the Simple Factory Pattern works in C# and .net core. The simple factory design pattern is a form of abstraction, which hides the actual logic of an implementation of an object so the initialization code can focus on usage, rather than the inner workings.

![Simple Factory Pattern](https://garywoodfine.com/wp-content/uploads/2018/08/SimpleFactoryPattern.jpg "Simple Factory Pattern")

In the diagram `Manufacture` is a base class, and classes `Chocolate` and `MotorVehicle` are derived from it, the `Manufacture` class decides which of the subclasses to return, depending on the arguments provided.

The developer using the `Manufacture` class doesn't really care which of the subclasses is returned, because each of them have the same methods but will have different implementations. How the factory class decides which one to return, is entirely up to what data is supplied. It could be very complex logic, however, most often it is very simple logic.

### Code Sample

To build a Hypothetical sample of a Simple Factory Pattern, consider a typical Firstname and Lastname scenario. If we consider we're going to build an application that will take a username string input from various applications. However, there is some inconsistency in how the string is supplied. In one application it is passed through as `"FirstName LastName"` and the other application it is passed through as `"LastName, FirstName"`.

We wouldn't want to pollute our code base with different string manipulation strategies when we could make use of different classes to handle this scenario based on the input received.

Deciding which version of the name to display can be a simple decision making use of the simple `If then` statement.

We'll start by defining a simple class that takes the username as string argument in the constructor and allows you to fetch the split names back.

First, we'll define a simple base class for the Username, consisting of the basic properties we need.

```csharp
public class UserName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```
#### Derived Classes

We can develop two very simple derived classes that implement the abstract class and split the username into two parts in the constructor.  In this example we'll base the class on the assumption that the username is separated by a space, when we need to use the `FirstName` first scenario

```c#

public class FirstNameFirst : UserName
    {
        public FirstNameFirst(string username)
        {
            var index = username.Trim().IndexOf(" ", StringComparison.Ordinal);

            if (index &lt;= 0) return;

            FirstName = username.Substring(0,index).Trim();
            LastName = username.Substring(index + 1).Trim();
        }
    }

```

In the second class, we'll base it on the assumption that the name is split by a comma.

```c#

public class LastNameFirst : UserName
    {
       public LastNameFirst(string username)
       {
            var index = username.Trim().IndexOf(",", StringComparison.Ordinal);

           if (index &lt;= 0) return;

           LastName = username.Substring(0,index).Trim();
           FirstName = username.Substring(index + 1).Trim();
       }
    }

```

### Build a Simple Factory Pattern

We'll build a Simple Factory which simply tests for the existence of comma and then return an instance of one class or the other.

```c#
public class UsernameFactory
    {
        public UserName GetUserName(string name)
        {
            if (name.Contains(",")) return new LastNameFirst(name);

            return new FirstNameFirst(name);
        }
    }
```
![Simple Factory Pattern](https://garywoodfine.com/wp-content/uploads/2018/08/UserNameFactory.jpg "Simple Factory Pattern")


### Unit Test Simple Factory Pattern

We can now simply test our factory pattern using xUnit unit testing framework as follows.

```c#
public class UsernameFactoryTests
    {
        private UsernameFactory _factory;

        public UsernameFactoryTests()
        {
            _factory = new UsernameFactory();
        }

        [Fact]
        public void ShouldGetFirstNameFirst()
        {
            //arrange
            var user = "Gary Woodfine";
           
            //act
            var username = _factory.GetUserName(user);


            //assert
            Assert.Equal("Gary", username.FirstName);
            Assert.Equal("Woodfine", username.LastName);

        }

        [Fact]
        public void ShouldGetLastNameFirst()
        {
            //arrange
            var user = "Woodfine, Gary";

            //act
            var username = _factory.GetUserName(user);


            //assert
            Assert.Equal("Gary", username.FirstName);
            Assert.Equal("Woodfine", username.LastName);

        }
    }
```

As you can see now when we make a call to `GetUserName` function we simply pass in the string and the factory method decides which version of the UserName instance to pass back to the application. The developer doesn't need to concern themselves with any of the string manipulation required. 

The `UserNameFactory` simple factory design pattern is a form of abstraction, which hides the actual logic of an implementation of an object so the initialization code can focus on usage, rather than the inner workings. provides us the detail we need, all we need to do is pass the name string in and the factory will determine which class to use to format the data.

### Summary

That is the fundamental principle of the Simple Factory Pattern, to create an abstraction that decides which of the several possible classes to return. A developer simply calls a method of the class without knowing the implementation detail or which subclass it actually uses to implement the logic. 

This approach helps to keep issues of data dependence separated from the classes' useful methods.

The post [Simple Factory Pattern](https://garywoodfine.com/simple-factory-pattern/) appeared first on [Gary Woodfine](https://garywoodfine.com) and is also available on [Dev.to](https://dev.to/gary_woodfine/simple-factory-pattern-in-c-and-net-core-3263)..

## Sponsored by 
[![threenine logo](http://static.threenine.co.uk/img/github_footer.png)](https://threenine.co.uk/)