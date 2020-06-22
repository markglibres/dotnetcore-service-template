

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

## The layers in detail
![enter image description here](https://raw.githubusercontent.com/markglibres/dotnetcore-api-template/master/assets/layers_detailed.jpg)

### Presentation
Consider this layer as the entry point of your application, may it be MVC app, API, GRpc, or a background service. For an API, this is where you receive the HTTP requests and sends the HTTP response. For a background worker, this is where you schedule or run a task. As a rule, you only expose presentation models to the consumers and not the ones coming from domain, application or infrastructure.  There should be no processing of business logics on this layer. It should only be mapping models required by application layer **(CQRS models)** and mapping of models returned by application layer through a mediator (will be explained below). Dependency injection **(IoC)** should also take place on this layer.

This will depend on Infrastructure and Application layer.

### Application
Command / Query Handlers - The main responsibility of this layer is to bridge the Presentation and Domain layer by using MediatR **(Mediator Pattern)**. The presentation layer will pass the commands or queries models, then the application layer will handle their execution. The command / query handler should only call interfaces of domain services **(Service layer pattern)**, application services and repositories. 

Integration services / events - This layer is also responsible for the integration to other services by sending Integration Events **(Event-Driven Design)**. This is where the Integration Events Service interface should reside. This layer knows what integration events to send to other services. 

Repository interfaces - This interface defines how data models interact with database (insert, save, delete, etc). 

Domain Event Handlers - The handlers for the emitted domain events should also reside on this layer. Same as the command / query handlers, it should only be calling interfaces of domain services, application services and repositories.

This layer should not expose or return domain models, but should map it to an application data model. 

This layer will have the Domain layer as its only dependency. 

### Domain
This layer does not have any dependency. It will contain the following:
* Domain models / entities / value objects  - The business data models. Should only have primitive types and not database specific fields. **(follow DDD)**
* Domain services - Applicable only if domain logic spans to multiple domain entities / models.
* Domain events - These are events emitted by the domain models. 

### Infrastructure
This layer is the implementation layer, which means, anything tech / implementation specific should reside on this layer. 

This layer should not contain any business specific logic. 

This also implements the application services such as integration event service with Azure Service Bus. 

This layer depends on Application and Domain. 

##
Follow this [link](https://github.com/markglibres/dotnetcore-service-template/wiki/How-to-install-template) on how to use the template for your project with included code samples. 

## Architectures
Usage of this project template can be applied to different architectures and provide flexibility when switching. See digrams below on how you can achieve from monolith to microservices:

### Modular Monolith
![Modular Monolith](https://github.com/markglibres/dotnetcore-service-template/blob/master/assets/Modular_Monolith.jpg?raw=true)

### Microservice - Choreography
![Choreography](https://github.com/markglibres/dotnetcore-service-template/blob/master/assets/Choreography_Microservice.jpg?raw=true)

### Microservice - Orchestrator
![Orchestrator](https://github.com/markglibres/dotnetcore-service-template/blob/master/assets/Orchestrator_Microservice.jpg?raw=true)

### Microservice - Hybrid
![Hybrid](https://github.com/markglibres/dotnetcore-service-template/blob/master/assets/Hybrid_Microservice.jpg?raw=true)
