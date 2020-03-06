![architecture layers](https://raw.githubusercontent.com/markglibres/dotnetcore-api-template/master/assets/layers.jpg)

An opinionated architecture inspired by ONION architecture, clean architecture, CQRS, DDD, service pattern, repository pattern, event driven design and mediator pattern…

## Project Structure (API) Guidelines

### A. Presentation

1.  Controllers
    * Calls mediator (Send / Publish of commands and queries)
    * Transform api requests objects to commands or queries
    * Transform application DTOs to api responses (i.e. hal or jsonapi)
    * Example:
    #### Example (controller)

    ```
    // controller 
    public Task<IActionResult> RegisterUser(FormRequest request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);
        var commandResponse = await _mediator.Send(command);

        var apiResponse = _mapper.Map<GenericHalResponse>(commandResponse);

        return Ok(apiResponse);
    }
    ```
        
2.  Mappers
    * Object mappers to and fro application DTOs
    
3.  Requests
    * Api request objects
        
4.  Responses
    * Api response objects
        
5.  Configurations
    * D.I. configurations
    * D.I. registrations
        

### B. Application

1.  Events - messages shared across different domains / microservices or within application layer
      - Handlers
         * should only call domain / application services
         * should not call concrete implementations
         * should not access repository services
         * can emit application events
      - Dtos
         * application events
      - Mappers
      
2.  Users {folder by feature}
      - Commands
         - Handlers
            * should only call domain / application services
            * should not call concrete implementations
            * should not access repository services
            * emits application events
         - Dtos
            * commands
            * responses
         - Mappers 
      - Queries
         - Handlers
         - Dtos
         - Mappers
                       

#### Example (command handler)

```
// RegisterUserCommandHandler
public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
{
    var registrationForm = _mapper.Map<RegistrationForm>(command);
    var registrationId = await _registrationService.Register(
        registrationForm.username,
        registrationForm.password, 
        registrationForm.email, 
        registrationForm.firstName, 
        registrationForm.lastName);

    // informs other microservices (i.e. analytics) that a successful registration has completed
    await _applicationEventsService.Publish(new SuccessfulRegistrationEvent(registrationId));

    var response = _mapper.Map<RegisterUserResponse>(RegistrationForm);
    response.RegistrationId = registrationId;

    return response;
}
```

#### Example (event handler)

```
// Registration Event handler
// UserRegisteredEvent is emitted by domain
public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
{
    var registration = await _registrationService.Get(@event.registrationId);
    var user = await _userService.Get(registration.UserId);
    
    // IEmailService will be in Application Layer but implemented
    // on Infrastructure layer
    await _emailService.SendWelcome(user);
}
```

### C. Domain

1. Entities
2. Value Objects
3. Aggregates
4. Events
   - domain events only
   - messages only used within the domain and not across different microservices / domain
4.  Seedwork - (interfaces and abstract classes)
   * Services
      - IDomainEventsService
   * Repositories
5.  Services
   - implementation of domain services
   - emits domain events by calling interface for domainEventsService (Send / Publish)
   - calls repository interfaces    

#### Example (interfaces)

```
// implementation will be on Domain layer
public interface IRegistrationService 
{
    Task<string> Register(
        string username,
        string password, 
        string email, 
        string firstName, 
        string lastName);
}

// implementation will be on Domain layer
public interface IUserService
{
    Task<User> Get(string userId);
    Task<bool> IsUserExists(string email);
}

// implementation will be on Infrastructure layer
public interface IRegistrationRepository
{
    Task<string> Insert(RegistrationForm form); 
}

// implementation will be on Infrastructure layer
public interface IUserRepository
{
  Task<User> GetByUserId(string userId);
}
```

#### Example (services)

```
public class RegistrationService: IRegistrationService
{
    private readonly IRegistrationRepository _repository;
    private readonly IUserService _userService;
    private readonly IDomainEventsService _domainEventsService;
    
    public RegistrationService(
        IRegistrationRepository repository,
        IUserService userService,
        IDomainEventsService domainEventsService
    )
    {
        _repository = repository;
        _userService = userService;
        _domainEventsService = domainEventsService;
    }
    
    public async Task<string> Register(
        string username,
        string password, 
        string email, 
        string firstName, 
        string lastName)
    {
        if(_userService.IsUserExists(email))
        {
            throw new UserAlreadyExistsException(email);
        }
        
        var user = _userService.Create(username, email, _hashService.Generate(password), firstName, lastName);
        var form = new RegistrationForm(user.Id);;
        var id = await _repository.Insert(form);
        
        await _domainEventsService.Publish(form.DomainEvents);
        
        return id.ToString();
    }
}
```

#### Example (entity and events)

```
public class RegistrationForm: IDomainEntity, IDomainEvents
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public DateTime DateCreated { get; }
    public IEnumerable<IDomainEvent> DomainEvents { get; private set; }
    
    public RegistrationForm(string userId)
    {
        Id = Guid.New();
        UserId = Guid.Parse(userId);
        DateCreated = DateTime.Now;
        
        DomainEvents.Add(new UserRegisteredEvent
        {
            UserId = UserId;
            RegistrationId = Id;
        });
    }
}
```

### D. Infrastructure

1.  Application - implementation of application services
    * Services
      - i.e. ApplicationEventsService
2.  Domain - implementation of domain services
    * Repositories
    * Services

#### Example (repository)

```
public class CosmosRegistrationRepository: IRegistrationRepository
{
    private readonly Container _container;
    
    public CosmosRegistrationRepository(
        IOptions<CosmosDbConfig> options,
        Database database)
    {
        var containerConfig = options.Value.Registration;
        _container = database.GetContainer(containerConfig.ContainerName);
    }
    
     public async Task<string> Insert(RegistrationForm form)
      {
          Guard.Against.MissingEmail(form.email);
          
          await _container.CreateItemAsync(form);
          
          return form.Id;
      }
}
```

