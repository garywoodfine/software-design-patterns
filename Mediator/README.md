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


I prefer to use MediatR as my implementation of choice of the Mediator Pattern, because of its ease of use and versatility. I also learned a lot about code by reading the MediatR source code. It is well worth cloning the repository and spending sometime just going through the implementation, it will provide one with a good sense of how to implement the pattern and also an appreciation of some of the finer points of the C# language.

###  Why use the Mediator pattern
When working with Domain Classes where multiple functions are used to modify state and implement domain rules it usually becomes difficult to debug, extend and review the implementation.  Often business rule functions implement multiple sub-rules that are repeatedly required elsewhere in the domain. This may lead to Multiple layers of complex abstraction required to share functionality or multiple strategies from several developers advocating different patterns and practices.

A common challenge in a codebase, is dealing with the constant flux of ever changing domain requirements and business rules.

Typically software services start out as simple CRUD applications, but gradually evolve to become complex as more rules and changes are introduced.

Developers may initially try to solve this problem, by implementing a *Manager* class to centralise the logic to contain every action possible to modify the state of an entity or to modify the state of another *manager* class.

Testing often becomes convoluted, messy, complex and usually incomplete. Typically the problems occur when in order to test the class there are a number of dependencies to mock and configure. Just to test a new function.

Typically no validation of the inbound request is implemented, and the request is allowed to pass through the middle layer, API layer and only validated within the Manager class functions. This often causes bloated action functions with complicated validation rules before the implementation logic!

### How does the Mediator pattern solve issues

The mediator pattern promotes loose coupling, by implementing a mediator object to enable objects to communicate with it rather than each other.

## Example of the mediator pattern using MediatR

In [Developing Api’s using Http Endpoints](https://garywoodfine.com/developing-apis-using-http-endpoints/) I discussed some of the problems relating to using MVC pattern to developing Web API projects and how to overcome by making use of the [Adralis API Endpoints](https://github.com/ardalis/ApiEndpoints) and I even created a [API Template project](https://garywoodfine.com/how-to-create-project-templates-in-net-core/) to help you get started. 

In this very simplified example we are going to make use of the template to create a basic project structure to illustrate how to use the Mediator Pattern, and we will also be making use of [MediatR](https://github.com/jbogard/MediatR/wiki), primarily because the template project comes pre-configured to use it.

Once you have [installed the template](https://www.nuget.org/packages/threenine.ApiProject/)  we can create a new project using

```c#
dotnet new apiproject -n mediator
```
Once the project has been generated, we will have have all that is required done for us to provide the most simplistic example of the Mediator pattern. 

**The Sample project is also generated using what is known as [Vertical Slice Architecture](https://jimmybogard.com/vertical-slice-architecture/) or Feature Slices which are Structural Pattern and methodology that aims for logic separation for components and library code**

It is important that the basis of the Mediator pattern, is Request & Response mediation or what is more commonly known as the [CQRS (*Command Query Responsibility Segregation*)](https://articles.geekiam.io/what-is-cqrs "What is CQRS | Geek.I.Am") . 

> Mediator promotes loose coupling by keeping objects from referring to each other explicitly and it lets you vary their interaction independently
> 
>  [Design Patterns: Elements of Reusable Object-Oriented Software](https://amzn.to/2PdkTck ) 


![Mediator Pattern](https://garywoodfine.com/wp-content/uploads/2021/05/mediator.png)

In order to speed things up even more, I am also going to use a few other products I have developed to quickly build out the other infrastructure details. Hence, I will be making use of the [ThreeNine.Data - Generic Repository](https://www.nuget.org/packages/Threenine.Data/ "Threenine.Data | Nuget") which I discussed in more detail in [Using the Repository and Unit Of Work Pattern in .net core](https://garywoodfine.com/generic-repository-pattern-net-core/) and also the [Code First Database library Nuget package](https://www.nuget.org/packages/Boleyn.Database.Postgre/) from our [Headless CMS](https://threenine.co.uk/what-is-a-headless-cms/) we are currently in the process of developing.

I will not discuss the whole process of setting up the project to use these libraries because the [source code](https://github.com/garywoodfine/software-design-patterns/tree/master/Mediator) is for this example is available. For the purpose of this article we will only discuss the Mediator pattern implementation.

We will implement functionality to Create a new Salutation, which is essentially a polite expression of greeting, which covers things like Mr., Mrs. , Prof. etc.  Our simplistic API endpoint is simply going to enable adding new items to the list.

In our application configuration in the `Startup.cs` we will first enable MediatR to essentially scan our assembly and automatically wire up all Mediator Handlers it finds ready to be used.  We'll do this by simply adding the `services.AddMediatR(typeof(Startup));` in our `ConfigureServices` method.

```c#
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "mediator", Version = "v1"});
                c.EnableAnnotations();
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<BoleynContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("mediator"))
            ).AddUnitOfWork<BoleynContext>();
        }
```

We'll create a new Api Endpoint as `Post` , the most important details are that we simply dependency inject `IMediator` object into our class, which has been made possible because of the step we carried out earlier of instructing MediatR to scan our assembly and wire up all the handlers.

You notice that we have configured our endpoint to Accept a request, which will contain a command, and that our endpoint will not provide a Response. Although it is still important to remember our Endpoint will still provide an ActionResult response, but in this instance it means that endpoint will not be providing an Object response.

```c#
 [Route(RouteNames.Salutations)]
    public class Post : BaseAsyncEndpoint.WithRequest<CreateSalutationCommand>.WithoutResponse
    {
        private readonly IMediator _mediator;

        public Post(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new salutation",
            Description = "",
            OperationId = "AA440D51-75A5-4975-8875-C1799B58D4EB",
            Tags = new []{RouteNames.Salutations}
            )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateSalutationCommand request, CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new CreatedResult( new Uri(RouteNames.Salutations, UriKind.Relative), new { id = result });
        }
    }
```
Our endpoint handler accepts a `CreateSalutationCommand` object, which in theory is an implementation of CQRS,  We need to create our  `CreateSalutationCommand` object as follows.


```c#
    public class CreateSalutationCommand : IRequest<int>
    {
        public string Abbreviation { get; set; }
        public string FullWord  { get; set; }
    }
```
You will notice our Command inherits from the `IRequest` essentially stipulate the Type that our object will request. This type can either a Primitive or Complex type, however in this case we are simply requesting an Integer return value containing the ID of the created object.

We can now configure out Handler object, you will notice that we create a `class` which inherits from the `IRequestHandler` interface and accepts the `CreateSalutationCommand` and provides an `int` response type.

We can inject whatever dependencies we may need into this class, in our case it will be the Mapper and Unit Of Work from our generic repository.

```c#
  public class PostHandler : IRequestHandler<CreateSalutationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateSalutationCommand request, CancellationToken cancellationToken)
        {
            var salutation = _mapper.Map<Salutation>(request);
            var repo = _unitOfWork.GetRepositoryAsync<Salutation>();
            await repo.InsertAsync(salutation, cancellationToken);
            await _unitOfWork.CommitAsync();
            return salutation.Id;
        }
    }
}
```
In this very simple example, you'll notice that we have placed our business logic all with `Handle` method, which I would be the first to admit is not great, but it also serves to illustrate that we have separated it from our Endpoint logic.

In later, articles based on MediatR, I will provide further examples of how this can further be improved enabling you to take further advantage of MediatR, but for now I just want to illustrate the Mediator Pattern at work.

The take away from this example, is that you will notice that at no point do we explicitly define a reference to our Handler anywhere, in our Post endpoint, we simply make use of the `Send` function available on the `IMediator` to send our request to whichever object has been defined to satisfy the Request and Response pairing.  The Mediator, then ensures it sends it to which ever it has been configured too.

### Conclusion
The most simplest definition of the Mediator pattern is that it is an object that encapsulates how objects interact. So it can obviously handle passing Request and Response between objects.

The Mediator pattern promotes loose coupling by not having objects refer to each other, but instead refer to the mediator. So they pass the messages to the mediator, who will pass it on to the object that has been defined to handle them.

The CQRS pattern defined actually just an “implementation” of the mediator pattern. As long as we are promoting loose coupling through a “mediator” class that can pass data back and forth so that the caller doesn’t need to know how things are being handled, then we can say we are implementing the Mediator Pattern.




