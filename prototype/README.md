# The Protoype Pattern C# .net core


The Prototype Design Pattern is one of the _Creational Design Patterns_ defined by the _Gang Of Four (GOF)_ published their book [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PwkRfA) in which they presented a catalog of simple and succinct solutions to commonly occurring design problems.

The Prototype pattern is specifically used when creating a duplicate object of an existing object while attempting to conserve resources and focus on performance.

For example, consider a real-world example of a Software Developer. In order to create or instantiate a new instance of Software Developer, we would in the first instance create a new Human Object, Nurture and Parent the Human object while educating it. Ensuring that the Human Object gets all the right attributes and elements which eventually lead to it becoming a Software Developer. 

Creating a Software Developer object, is a very resource intensive and time consuming process. What happens you need another 10 of these objects ?  Wouldn't it be great if we could just clone our existing copy of the software developer, which already has all the attributes and properties we need instantly?

This is the type pf scenario that Prototype Pattern serves to address.  The pattern provides a prototype interface which enables creating a clone of the current object. This pattern is used when creation of object directly is costly. For example, an object is to be created after a costly database operation. We can cache the object, returns its clone on next request and update the database as and when needed thus reducing database calls.

## Prototype Pattern Implementation in C#
A typical implementation of the Prototype pattern could modelled as follows

![Prototype pattern](https://garywoodfine.com/wp-content/uploads/2019/10/Prototype-2.png)

A class may implement an ICloneable Interface which requires the class to implement a Clone method.

The .net framework provides developers with the [ICloneable Interface](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable?view=netcore-3.0) 
 
 >The ICloneable interface enables you to provide a customized implementation that creates a copy of an existing object. The ICloneable interface contains one member, the Clone method, which is intended to provide cloning support beyond that supplied by Object.MemberwiseClone.
>

The ICloneable interface simply requires that your implementation of the Clone() method return a copy of the current object instance.

The developer is free to choose any implementation to perform the clone operation i.e. Deep, Shallow or Custom copy of the object.

## Practical Implementation of Prototype Pattern in C#

In the first simple implementation of the Prototype Pattern, we'll create a Developer class that implements `ICloneable` interface which preforms a [MemberwiseClone](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=netcore-3.0) operation.

The MemberwiseClone method creates a shallow copy by creating a new object, and then copying the nonstatic fields of the current object to the new object.

```c#

public class Developer : ICloneable
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string[] Skills { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

```  

We can now implement our basic client App to instantiate an initial Developer object then we will create a Second version of the object making use of the Clone method that is available.  We will then just inspect the cloned developer to check the FirstName it has been defined.

```c#
 class Program
    {
        static void Main(string[] args)
        {

            var dev = new Developer
            {
                FirstName = "Gary",
                Lastname = "Woodfine",
                Skills = new List<string> {"C#", "PHP", "SQL", "JavaScript"}
            };


            var dev2 = dev.Clone() as Developer; 
            
            Console.WriteLine($"The Cloned  Developer name is { dev2.FirstName }  { dev2.Lastname }");
            
            Console.WriteLine("The second developer has the following skills: ");


            foreach (var skill in dev2.Skills)
            {
                Console.WriteLine(skill);
            }
        }
    }
```
If you run the application you'll notice that the Second instance of the Developer has all the same properties of the first one instantiated.


## Applications for the Prototype Pattern in C#

The Prototype Pattern is useful when you need to be able to quickly create new instances of objects based on other objects. 

The following are typical application scenarios where you may want to consider using the Prototype pattern in C#


* You want to instantiate classes at run time, for example, by dynamic loading.
* Avoid building a class hierarchy of factories that parallels the class hierarchy of products.
* When new instantiations of class can have one of only a few different combinations of state.
* New object are going to be clones of existing object.
* Avoid subclasses of an object creator in the client application i.e.  [Abstract Factory Pattern](https://garywoodfine.com/abstract-factory-design-pattern/).
* Avoid resource intensive object instantiation and initialisation logic.

## Advantages of the Prototype Design Pattern

* Reduce the time complexity to creating resource consuming objects by using the prototype pattern.
* Reduces the sub-classing.
* Enables adding and removing objects at run time.
* Enables configuring application classes dynamically.

The Prototype Pattern is another tool you can use when you can specify the general class needed in program but need to defer the exact class until execution time. It is similar to the [Builder Pattern](https://garywoodfine.com/the-builder-pattern-net-core/)  in that some class or method decides what components or details make up the final instantiated class. However, it differs in that the target classes are constructed by cloning one or more classes and then changing or filling in the details of the cloned class to behave as desired.

Prototypes can be used whenever you need classes that differ only in the type of processing they offer.

Any change you make in one clone of the object is immediately reflected in the other because in fact there is only one object.  

To see this behaviour lets add some additional logic to our code, to add an additional skill to our Second Developer instance. After that lets print out our skills of our initial instance to the console.

```c#
 class Program
    {
        static void Main(string[] args)
        {

            var dev = new Developer
            {
                FirstName = "Gary",
                Lastname = "Woodfine",
                Skills = new List<string>{"C#", "PHP", "SQL", "JavaScript"}
            };


            var dev2 = dev.Clone() as Developer; 
            
            Console.WriteLine($"The Cloned  Developer name is { dev2.FirstName }  { dev2.Lastname }");
            
            Console.WriteLine("The second developer has the following skills: ");


            foreach (var skill in dev2.Skills)
            {
                Console.WriteLine(skill);
            }

            // Add a new Skill to our Cloned Instance
            dev2.Skills.Add( "VueJs");
            
            Console.WriteLine(" ");
            

            Console.WriteLine("Our Initial Developer object now has VueJS added too");
            foreach (var skill in dev.Skills)
            {
                 Console.WriteLine(skill);
            }
            
        }
```
If we run our sample we'll see that the `Vue.JS` has been added to our Developer instance skill set.

```text
The Cloned  Developer name is Gary  Woodfine
The second developer has the following skills: 
C#
PHP
SQL
JavaScript
 
Our Initial Developer object now has VueJS added too
C#
PHP
SQL
JavaScript
VueJs

``` 


 ## Sponsored by 
 [![threenine logo](http://static.threenine.co.uk/img/github_footer.png)](https://threenine.co.uk/)


 