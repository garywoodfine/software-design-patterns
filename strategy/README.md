# Strategy Pattern

The *Strategy Pattern* is a *Behavioral Pattern* , [Software Design Patterns](https://garywoodfine.com/software-design-patterns/ "Software Design Patterns | Gary Woodfine") defined by the Gang of Four in [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) , it is also discussed in [Head First Design Patterns](https://amzn.to/32YhjIT "Head First Design Patterns"). 

The Strategy pattern enables the definition and encapsulation of a number of algorithms and make them interchangeable. The algorithms may vary independently and may deliver different outcomes and enable different behaviour, however they can all be called using the same method signature.

A typical example of how the Strategy pattern is implemented is illustrated in the diagram below. 

![Stratey Pattern](https://garywoodfine.com/wp-content/uploads/2021/06/image.png)

In Strategy pattern, a class behaviour or its algorithm can be changed at run time. Enabling the creation of objects which represent various strategies and a context object whose behaviour varies as per its strategy object. The strategy object changes the executing algorithm of the context object.

Another name for the Strategy pattern is *Policy*, which in my opinion provides a better clue on how this pattern can be utilised in software applications.  The most common scenario in enterpise software development where the Strategy pattern can be implemented in a Business Rules Engine.

A business rules engine (BRE) is an application that manages decision processes using pre-defined logic to determine outcomes. BREs enable precise decision making, and are especially useful for complex dependencies, as well as in instances where regulatory or organizational rule changes frequently require logic changes.  

An example of this may be in a Bank loan software application, there are various rules that fire based on responses to questions. If the answer is *yes* to one question or *no* to another question, the chances of approval for a loan will be impacted, and the subsequent questions will vary. These rules help guide users through the application process and help the company determine the eligibility and structure their business.

I recently came across [Basic Rules Engine Design Pattern](https://tenmilesquare.com/basic-rules-engine-design-pattern/ "Basic Rules Engine Design Pattern | ten mile square"), where in the author just actually describes the typical implementation of the Strategy pattern, as you'll be able to tell from the class diagram he supplied, the implementation resembles the simplified Strategy Pattern above.

![Basic Rules Engine](https://garywoodfine.com/wp-content/uploads/2021/06/image-1.png "Basic Rules Engine")