# dotnetcore-api-template

A C# template for building API projects with:
  * IoC (inversion of control)
  * service pattern
  * repository pattern
  * mediator pattern
  * event driven design
  * CQRS
  * DDD

# Layers
  * Infrastructure layer - systems layer. i.e. emailer, logging. No knowledge of other layers
  * Domain layer - domain objects, domain events. Only has knowledge of the infastructure layer but only interfaces.
  * Data layer - repository. Only has knowledge of Infra and Domain layer but only interfaces.
  * Service layer - services. Only has knowledge of Infra, Domain and Data but only interfaces.
  * Application layer - api. Has knowledge of all layers and where dependency injection happens. This is the only layer that has knowledge of the implementation classes.

# Project Structure
  * Infrastructure - infra layer
  * Domain - domain layer
  * Data - data layer
  * Service - service layer
  * API - application layer. CQRS models, command, query and event handlers.
  * Core - implementation layer
  * Tests
    * Unit tests - unit testing
    * Component tests - testing end to end of API endpoints with stub data on calling external apis (if available).
    * Integration tests - testing of class implementations for calling external apis (if available).
  

 
