![architecture layers](https://raw.githubusercontent.com/markglibres/dotnetcore-api-template/master/assets/layers.jpg)

A simple architecture inspired by ONION architecture, clean architecture, CQRS, DDD, service pattern, repository pattern, event driven design and mediator pattern…

## Project Structure (API)

### A. Presentation

1.  Controllers
    
    1.  calls mediator (Send / Publish commands or queries)
        
    2.  transform api requests objects to commands / queries
        
    3.  transform application dtos to api responses (hal object)
        
2.  Mappers
    
3.  Requests
    
    1.  api requests
        
4.  Responses
    
    1.  api responses
        
5.  Configuration
    
    1.  dependency injection
        
    2.  injects implementation from Infrastructure layer
        

#### Example (controller)

```
// controller 
public Task<IActionResult> CreateForm(FormRequest request)
{
    var createFormCommand = _mapper.Map<CreateFormCommand>(request);
    var createFormResponse = await _mediator.Send(createFormCommand);
    
    var apiResponse = _mapper.Map<GenericHalResponse>(createFormResponse);
    
    return Ok(apiResponse);
}
```

### B. Application

1.  <Feature folder>
    
    1.  Commands
        
        1.  Handlers
            
            1.  should only call service interfaces from domain
                
        2.  Mappers
            
        3.  Dtos
            
            1.  commands
                
            2.  responses
                
    2.  Queries
        
        1.  Handlers
            
            1.  should only call service interfaces from domain
                
        2.  Mappers
            
        3.  Dtos
            
            1.  commands
                
            2.  responses
                
2.  Events
    
    1.  Handlers
        
    2.  Dtos
        

#### Example (command handler)

```
// CreateFormCommandHandler
public async Task<CreateFormResponse> Handle(CreateFormCommand command, CancellationToken cancellationToken)
{
    var registrationForm = _mapper.Map<RegistrationForm>(command);
    var registrationId = await _formService.Register(
        registrationForm.username,
        registrationForm.password, 
        registrationForm.email, 
        registrationForm.firstName, 
        registrationForm.lastName);
    
    // get and publish UserRegisteredEvent
    // IDomainEventsService is within Application layer but implementation
    // will be in Infrastructure layer
    await _domainEventsService.Publish(registrationForm.DomainEvents);
    
    // or use Automapper
    var response = new CreateFormResponse
    {
       RegistrationId = registrationId
    }    
    
    return response;
}
```

#### Example (event handler)

```
// Registration Event handler
public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
{
    var user = await _userService.Get(@event.UserId);
    
    // IEmailService will be in Application Layer but implemented
    // on Infrastructure layer
    await _emailService.SendWelcome(user);
}
```

### C. Domain

1.  Entities
    
2.  Value Objects - for DDD
    
3.  Aggregates - for DD
    
4.  Seedwork (interfaces and abstract classes)
    
    1.  Services
        
    2.  Repositories
        
5.  Services - implementation of domain services
    

#### Example (interfaces)

```
// implementation will be on Domain layer
public interface IFormService 
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
    public RegistrationService(
        IRegistrationRepository repository,
        IUserService userService
    )
    {
        _repository = repository;
        _userService = userService;
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
        
        var form = new RegistrationForm(
          username, _hashService.Generate(password),
          email, firstName, lastName
        );
        
        var id = await _repository.Insert(form);
        
        return id.ToString();
    }
}
```

#### Example (entity and events)

```
public class RegistrationForm: IDomainEntity, IDomainEvents
{
    public Guid Id { get; }
    public string Username { get; }
    public string Password { get; }
    public string Email {get; }
    public string Firstname {get; }
    public string Lastname {get; }
    public IEnumerable<IDomainEvent> DomainEvents { get; private set; }
    
    public RegistrationForm(
        string username,
        string password, 
        string email, 
        string firstName, 
        string lastName)
    {
        Id = Guid.New().ToString();
        Username = username;
        Password = password;
        Email = email;
        Firstname = firstName;
        Lastname = lastName;
        
        DomainEvents.Add(new UserRegisteredEvent
        {
            UserId = Id;
        });
    }
}
```

### D. Infrastructure

1.  Application
    
    1.  Services
        
2.  Domain
    
    1.  Repositories
        
    2.  Services
        

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
```A simple architecture inspired by ONION architecture, clean architecture, CQRS, DDD, service pattern, repository pattern, event driven design and mediator pattern…

## Project Structure (API)

### A. Presentation

1.  Controllers
    
    1.  calls mediator (Send / Publish commands or queries)
        
    2.  transform api requests objects to commands / queries
        
    3.  transform application dtos to api responses (hal object)
        
2.  Mappers
    
3.  Requests
    
    1.  api requests
        
4.  Responses
    
    1.  api responses
        
5.  Configuration
    
    1.  dependency injection
        
    2.  injects implementation from Infrastructure layer
        

#### Example (controller)

```
// controller 
public Task<IActionResult> CreateForm(FormRequest request)
{
    var createFormCommand = _mapper.Map<CreateFormCommand>(request);
    var createFormResponse = await _mediator.Send(createFormCommand);
    
    var apiResponse = _mapper.Map<GenericHalResponse>(createFormResponse);
    
    return Ok(apiResponse);
}
```

### B. Application

1.  <Feature folder>
    
    1.  Commands
        
        1.  Handlers
            
            1.  should only call service interfaces from domain
                
        2.  Mappers
            
        3.  Dtos
            
            1.  commands
                
            2.  responses
                
    2.  Queries
        
        1.  Handlers
            
            1.  should only call service interfaces from domain
                
        2.  Mappers
            
        3.  Dtos
            
            1.  commands
                
            2.  responses
                
2.  Events
    
    1.  Handlers
        
    2.  Dtos
        

#### Example (command handler)

```
// CreateFormCommandHandler
public async Task<CreateFormResponse> Handle(CreateFormCommand command, CancellationToken cancellationToken)
{
    var registrationForm = _mapper.Map<RegistrationForm>(command);
    var registrationId = await _formService.Register(
        registrationForm.username,
        registrationForm.password, 
        registrationForm.email, 
        registrationForm.firstName, 
        registrationForm.lastName);
    
    // get and publish UserRegisteredEvent
    // IDomainEventsService is within Application layer but implementation
    // will be in Infrastructure layer
    await _domainEventsService.Publish(registrationForm.DomainEvents);
    
    // or use Automapper
    var response = new CreateFormResponse
    {
       RegistrationId = registrationId
    }    
    
    return response;
}
```

#### Example (event handler)

```
// Registration Event handler
public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
{
    var user = await _userService.Get(@event.UserId);
    
    // IEmailService will be in Application Layer but implemented
    // on Infrastructure layer
    await _emailService.SendWelcome(user);
}
```

### C. Domain

1.  Entities
    
2.  Value Objects - for DDD
    
3.  Aggregates - for DD
    
4.  Seedwork (interfaces and abstract classes)
    
    1.  Services
        
    2.  Repositories
        
5.  Services - implementation of domain services
    

#### Example (interfaces)

```
// implementation will be on Domain layer
public interface IFormService 
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
    public RegistrationService(
        IRegistrationRepository repository,
        IUserService userService
    )
    {
        _repository = repository;
        _userService = userService;
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
        
        var form = new RegistrationForm(
          username, _hashService.Generate(password),
          email, firstName, lastName
        );
        
        var id = await _repository.Insert(form);
        
        return id.ToString();
    }
}
```

#### Example (entity and events)

```
public class RegistrationForm: IDomainEntity, IDomainEvents
{
    public Guid Id { get; }
    public string Username { get; }
    public string Password { get; }
    public string Email {get; }
    public string Firstname {get; }
    public string Lastname {get; }
    public IEnumerable<IDomainEvent> DomainEvents { get; private set; }
    
    public RegistrationForm(
        string username,
        string password, 
        string email, 
        string firstName, 
        string lastName)
    {
        Id = Guid.New().ToString();
        Username = username;
        Password = password;
        Email = email;
        Firstname = firstName;
        Lastname = lastName;
        
        DomainEvents.Add(new UserRegisteredEvent
        {
            UserId = Id;
        });
    }
}
```

### D. Infrastructure

1.  Application
    
    1.  Services
        
2.  Domain
    
    1.  Repositories
        
    2.  Services
        

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
