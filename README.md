# dotnetcore-api-template

A C# template for building API projects with:
  * IoC (inversion of control)
  * service pattern
  * repository pattern
  * mediator pattern
  * event driven design
  * DDD (Aggregate pattern)
  * CQRS
    * Commands - use aggregates 
    * Queries - do not use aggregates
  
# Layers
  * Domain layer - domain entities + data + behaviour, aggregates, domain events, repository interfaces
    * Entities - have unique identifiers
    * Value Objects - immutable. cannot be changed once created.
    * Aggregates - composition of multiple entities
    * Repository interface
      * Commands - use aggregates. transactional data.
      * Queries - use simple and fast ORM such as Dapper. it is recommended to use dynamic types.
  * Infrastructure layer - repository implementation, data persistence layer, logging, etc. Only has knowledge of Domain layer but only through interfaces.
  * Service layer - domain and application logic services. Only has knowledge of Domain and Infrastruture layer through interfaces.
  * Application layer - Only call the service layer. This is the only layer that has knowledge of the implementation classes. Mapping of domain/data models to view models. This defines the api data contract.
  
# Project Structure
  * Domain  - domain layer
  * Core - infra layer
  * Service - service layer
  * API - application layer. CQRS models, command, query and event handlers
    * {feature name} - folder based on feature name
      * Commands - returns view models and not domain models. command models / handlers
      * Queries - returns view models and not domain models. query models / handlers
      * Controllers
      * EventHandlers - domain event handlers
    * Mappers - mapping of response models to api contracts if applicable
    
  * Tests
    * Unit tests - unit testing
    * Component tests - testing end to end of API endpoints with stub data on calling external apis (if available).
    * Integration tests - testing of class implementations for calling external apis (if available).
  

 
