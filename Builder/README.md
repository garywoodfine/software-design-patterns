
The Builder Pattern is a creational Gang of Four (GoF) design pattern,  defined in their seminal book 
[ Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2N22a2H), in which they presented a 
catalogue of simple and succinct solutions to commonly occurring design problems.

The pattern is useful for encapsulating and abstracting the creation of objects. It is distinct from the more common 
[Factory Pattern](https://garywoodfine.com/factory-method-design-pattern/) because the Builder Pattern contains methods
 of customising the creation of an object.

Whenever an object can be configured in multiple ways across multiple dimensions, the Builder Pattern can simplify the
 creation of objects and clarify the intent. 

![Builder Pattern](https://garywoodfine.com/wp-content/uploads/2018/11/BuilderPattern.png)

Let's explore the Builder Pattern and how developers can use it to construct objects from components.  You may have
 already seen that the [Factory Pattern](https://garywoodfine.com/factory-method-design-pattern/) returns one of 
 several different subclasses, depending on the data passed in arguments to creation methods.
 
We'll now learn that the Builder Pattern assembles a number of objects in various ways depending on the data.

### Advantages of the Builder Pattern
* The builder pattern enables developers to hide details of how an object is created
* The builder pattern enables developers to vary the internal representation of an object it builds.
* Each specific builder is independent of others and the rest of the application, improving Modularity and simplifies 
and enables the addition of other Builders.
* Provides greater control over the creation of objects.

The builder pattern is similar to the Abstract Factory Pattern in that both return classes made up of a number of 
other methods and objects. 

The main difference between the Builder Pattern and the Abstract Factory Pattern, is that the Abstract Factory Pattern 
returns a family of related classes and the Builder Pattern constructs a complex object step by step, depending on the
 data presented to it.

### Builder Pattern in Unit tests
The builder pattern is a popular pattern to use in Unit tests, in fact one of my favourite tools to use in 
Unit Tests is [Nbuilder - A rapid test object generator](https://github.com/nbuilder/nbuilder), which if you read 
the source code also provides a great example of how to implement the builder pattern.

In his book [Adaptive Code](https://amzn.to/2VyXJAN) Gary Maclean Hall states the builder pattern is useful for 
encapsulating and abstracting the creation of objects, and provides an example of using the builder pattern to help 
clarify the intent of unit tests, by assisting to eliminate any unnecessary arrange code.


### Example Builder Pattern

{% github garywoodfine/software-design-patterns %}

In this example, we are going to implement a very simple Builder Pattern and use it to create a Person class to 
contain some attributes to describe a person

```c#
  public class Person
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
    }


```

You may notice that we make use of an Enum to contain the value of the gender. 

```c#
 public enum Gender
    {
        Male,
        Female
    }
```
There is nothing all that complicated about the class, it's a simple POCO class.  We can now develop our Builder class,
 which again we will keep simple to help illustrate the point. The builder class will basically return the object 
 in a string format.

```c#
 public class Person
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Occupation { get; set; }

        public Gender Gender { get; set; }

        public override string ToString()
        {
            return  $"Person with id: {Id}  with date of birth {DateOfBirth.ToLongDateString()}   
            and name {string.Concat(Firstname, " ",Lastname)} is a {Occupation}";
        }
    }

```

The builder class in its simplest guise just a series of name constructor methods with arguments, 
you'll notice that they always return an instance of the class.  The final method on the builder class is `Build` 
method will return the completed object. By convention this method is typically named `Build` or `Create`  
or something similar.

We can now make use of our Builder to create a person as follows.

```c#
   class Program
     {
         static void Main(string[] args)
         {
             
             var person = new PersonBuilder()
                 .Id(10)
                 .Firstname("Gary")
                 .Lastname("Woodfine")
                 .Gender(Gender.Male)
                 .DateOfBirth(DateTime.Now)
                 .Occupation("Freelance Full-Stack Developer")
                 .Build();
             
             Console.WriteLine(person.ToString());
            
             Console.ReadLine();
         }
     }

```
We build the object by instantiation the PersonBuilder then adding the properties, then the final method we call is 
the `Build` method.  We then simply call the `ToString()` method to write out our values.

Using the Builder Pattern, we can avoid using large constructor methods to provide all the required parameters for 
constructing our object. Large constructor methods lead to unreadable and difficult to maintain code.  It is possible 
that there may not always be the need to supply all arguments in constructor methods because in all probability they 
won't always be needed.

 In the above code, I intentionally introduced a code smell, in the `ToString()`, you'll notice there is a lot of 
 string interpolation and even additional concatenation. I primarily because I wanted to highlight how the .net core 
 makes use of the builder pattern.
 
 We can make use of the StringBuilder class, StringBuilder prevents having to recreate a string each time you
  are adding to it. Using the String class in C# means you are using an immutable object, but StringBuilder is much 
  faster in most cases since it's not having to create a new String each time you append to it.
 
 We can now refactor our `ToString()` method as follows.
 
 ```c#
public override string ToString()=>
         new StringBuilder()
            .Append("Person with id: ")
            .Append(Id.ToString())
            .Append("with date of birth ")
            .Append(DateOfBirth.ToLongDateString())
            .Append(" and name ")
            .Append(Firstname)
            .Append(" ")
            .Append(Lastname)
            .Append(" is a ")
            .Append(Occupation)
            .ToString();
```
 We use the StringBuilder to create the string. You'll notice, that even though I said by convention you could use 
 the `Build` or `Create` to define the method that will return your object, but you don't really need to rather you 
 could opt for another name, in the case of StringBuilder it is `ToString()` 
 

 
 In the above example we have implemented a simple builder patter, however it probably isn't easy to determine why this 
 actually provides any benefit to developers. After all,from this simple implementation you might be thinking but surely
 we could just a simply use C# object initialisation and get exactly the result.
 
 ```c#
 var person2 = new Person
             {
                 Id = 10,
                 Firstname = "Gary",
                 Lastname = "Woodfine",
                 DateOfBirth = DateTime.Now,
                 Occupation = "Freelance Full Stack Developer",
                 Gender = Gender.Male
             };
  
 ```
 The problem with this approach is that it is vry similar to passing arguments to a function, which inadvertently adds complexity to understanding the code.
 
 >The ideal number of arguments for a function is zero (niladic). Next comes one (monadic), followed closely by two (dyadic). Three arguments (triadic) should be avoided when possible. More than three (polyadic) requires very special justification – and then shouldn’t be used anyway.
 
 > [Uncle Bob - Clean Code](https://amzn.to/2PTO6ar)

There will invariably be situations when instantiating objects that you will need to call a function to do something to provide a value to that object. i.e. Generate a new ID which may need calling out to function to get a newly created Id value etc.

It is in situations like this that make the Builder pattern a much more viable option, and as defined in [Philosophy of Software Design](https://garywoodfine.com/philosophy-of-software-design/) we are able to pull complexity downwards.

> When developing a module, look for opportunities to take a little bit of extra suffering upon yourself in order to reduce the suffering of your users.
> John Ousterhout - [Philosophy of Software Design](https://amzn.to/2V8AHfC)
 
  ### Fluent Builder Pattern implementation
  
 The standard definition the Builder pattern separates the construction of a complex object from its representation so 
 that the same construction process can create different representations.
 
 The Builder pattern provides step-by-step creation of a complex object so that the same construction process can 
 create different representations is the routine in the builder pattern that also makes for finer control over the 
 construction process. All the different builders generally inherit from an abstract builder class that declares the g
 eneral functions to be used by the director to let the builder create the product in parts.
 
 Builder has a similar motivation to the [abstract factory](https://garywoodfine.com/abstract-factory-design-pattern/) but, whereas in that pattern, the client uses the 
 abstract factory class methods to create its own object, in Builder the client instructs the builder class on how to 
 create the object and then asks it for the result. How the class is put together is up to the Builder class. It's a 
 subtle difference.
 
 The Builder pattern is applicable when the algorithm for creating a complex object should be independent of the parts 
 that make up the object and how they are assembled and the construction process must allow different representations 
 for the object constructed.
 
 If we consider a person object and think of all the different variations we could expect to create a person object. 
 For instance, how do we deal with married woman? Considering in some cases we may need to take into consideration her
 maiden name.  How do we cater for trans-gender people?  It soon becomes clear that there are all manner of rules and 
 variations we need to consider when build a person object.  All manner of varying combinations and all sorts of 
 additional properties we will need to include. Rules that may not be easy or convenient to incorporate in object
 initialisation.
 
 In our first implementation of builder for a fluent interface of the Person class, we implemented the builder with no 
 strings attached. We have not enforced rules for the order of assignment. 
 
 The code is simple and easy to understand but it does leave the builder class open to misuse. We can implement the 
 basic expression builder with method chaining in C# .NET.
 
 We're going to refactor our builder slightly to incorporate a new method `Create` which will accept Firstname and 
 Lastname argument but more importantly we are going to remove the creation of the `Person` class from the constructor
 and into `Create` method.
 
 It also doesn't make much sense providing and Id to an object on creation, it is highly likely that a new Id should be
 created when the object is created. So we'll also remove the Id parameter from the Builder.
 
 Implementing a fluent interface is a relatively straight-forward task that can be done through the use of 
 method chaining. Method chaining is simply a set of assignment methods that return itself.   The result from each 
 method can then call the next assignment method, and so-on. 
  
  To guide the user and enforce rules of construction (such as, the Class can only be assigned once, 
  followed by attributes), we utilize progressive interfaces. Where the method would return “this”, we instead return 
  an interface for the next step in line.
  
  ```c#
   public class PersonBuilder
      {
          private Person _person;
  
         public PersonBuilder Create(string firstName, string lastName)
          {
              _person = new Person();
              _person.Firstname = firstName;
              _person.Lastname = lastName;
              _person.Id = Guid.NewGuid();
              return this;
  
          }
          public PersonBuilder DateOfBirth( DateTime dob)
          {
              _person.DateOfBirth = dob;
              return this;
          }
  
          public PersonBuilder Gender(Gender gender)
          {
              _person.Gender = gender;
              return this;
          }
  
          public PersonBuilder Occupation(string occupation)
          {
              _person.Occupation = occupation;
              return this;
          }
          
          public Person Build()
          {
              return _person;
          }
      }
  
  ```
  
  we’ve implemented the Expression Builder pattern using method chaining. The class itself constructs a Person 
  for us. We may then call any of the assignment methods to populate the fields and attributes of the Character class. 
  Each method returns a copy of itself, allowing us to chain the assignment methods, one after the other, 
  thus implementing our fluent interface in C# .NET. We can then finally call the `Build()` method to obtain the 
  completed Person class.
  
### Summary
 
 We examined the Builder Pattern and seen how useful it is too create complex objects.  We also looked at an example
 how the .net core framework itself makes use of the builder pattern to provide common functionality string building 
 functionality.
 

 ## Sponsored by 
 [![threenine logo](http://static.threenine.co.uk/img/github_footer.png)](https://threenine.co.uk/)
 