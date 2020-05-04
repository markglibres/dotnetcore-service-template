# DotNetCore Service Project Template

This is a C# project template for an api / grpc / background service app. The goal is to create a backend microservice in just a few minutes with a clean and loosely-coupled architecture. The idea came up after working with choreography and orchestrator microservices pattern, modular monolith, monolith, and tightly-coupled microservices projects. 

The template combines the following patterns / architecture / design:
* IoC (dependency injection)
* Event-driven design
* CQRS
* Service layer pattern
* Repository pattern
* Mediator pattern
* Clean architecture
* Domain driven design

## Prerequisites
* dotnetcore sdk 3.1 <
* visual studio 2019 <
* mssql server (or docker - run mssql on docker)
* azure service bus (optional)

## The layers
![enter image description here](https://raw.githubusercontent.com/markglibres/dotnetcore-api-template/master/assets/layers.jpg)

### Presentation
Consider this layer as the entry point of your application, may it be MVC app, API, GRpc, or a background service. For an API, this is where you receive the HTTP requests and sends the HTTP response. For a background worker, this is where you schedule or run a task. As a rule, you only expose presentation models to the consumers and not the ones coming from domain, application or infrastructure.  There should be no processing of business logics on this layer. It should only be mapping models required by application layer **(CQRS models)** and mapping of models returned by application layer through a mediator (will be explained below). Dependency injection **(IoC)** should also take place on this layer.

This will depend on Infrastructure and Application layer.

### Application
The main responsibility of this layer is to bridge the Presentation and Domain layer by using MediatR **(Mediator Pattern)**. The presentation layer will pass the commands or queries models, then the application layer will handle their execution. The command / query handler should only call interfaces of domain services **(Service layer pattern)**, but never the repository interfaces. 

This layer is also responsible for the integration to other services by sending Integration Events **(Event-Driven Design)**. This is where the Integration Events Service interface should reside. This layer knows what integration events to send to other services. 

Handlers for the emitted domain events should also reside on this layer. Same as the command / query handlers, it should only be calling domain services but not the repository interfaces. 

This layer should not expose or return domain models, but should map it to an application data model. It shouldn't directly call domain model methods, it has to go through the domain service.

This layer will have the Domain layer as its only dependency. 

### Domain
This layer does not have any dependency. It will contain the following:
* Domain models - The business data models. Should not define database specific fields. **(follow DDD)**
* Domain services - This contains the business logic. This is where "servicing" of domain models happens and calling the repository to create, update, delete and publishing of domain events. (by calling repository and domain events service interfaces)
* Domain events - These are events emitted by the domain models. They are not created by the domain services, but they are extracted from the data models after "servicing". 
* Repository interfaces - This interface defines how data models interact with database (insert, save, delete, etc). For simplicity, repository found on this template does not separate the command / query responsibility. 
* Domain events service interface - This is an interface for publishing domain events.

### Infrastructure
This layer is the implementation layer, which means, anything tech / implementation specific should reside on this layer. While the domain defines the repository interfaces and domain events service interface, Infrastructure layer will implement it. For example, your repository can be on MSSQL using EFCore or Azure CosmosDB.

This layer should not contain any business specific logic. 

This also implements the application services such as integration event service with Azure Service Bus. 

This layer depends on Application and Domain. 


##
Follow this [link](https://github.com/markglibres/dotnetcore-service-template/wiki/How-to-install-template) on how to use the template for your project with included code samples. 
