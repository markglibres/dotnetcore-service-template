# dotnetcore-api-template

A C# template for building API projects with:
  * Docker
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
  * Domain layer
    * Entities (data + behaviour) - have unique identifiers
    * Value Objects - immutable. cannot be changed once created.
    * Aggregates - composition of multiple entities
    * Domain events
    * Repository interface
      * Commands - use aggregates. transactional data.
      * Queries - use simple and fast ORM such as Dapper. it is recommended to use dynamic types.
    * Domain services
      * domain related logic
      
  * Infrastructure layer - Only has knowledge of Domain layer but only through interfaces.
    * repository implementation
    * data persistence layer
    * logging
    * etc. 
    
  * Application layer - This is the only layer that has knowledge of the implementation classes. 
    * Dependency injection
    * Mapping of domain/data models to view models. 
    * Application services
              
# Project Structure
  * Domain
    * Entities
    * Value Objects
    * Aggregates
    * Events
    * Seedwork - abstracts, base, and interfaces
    * Services
    
  * Core - infra layer
    * Repository
        
  * Application
    * Root
      * API
        * Controllers
        * Mappers - mapping of response models to api contracts
    * Features
      * {feature name} - folder based on feature name
        * Commands - returns view models and not domain models
          * Models
          * Handlers
        * Queries - returns view models and not domain models
          * Models
          * Handlers
    * Handlers
      * EventHandlers - domain event handlers
        
  * Tests
    * Unit tests - unit testing
    * Component tests - testing end to end of API endpoints with stub data on calling external apis (if available).
    * Integration tests - testing of class implementations for calling external apis (if available).
  

 
