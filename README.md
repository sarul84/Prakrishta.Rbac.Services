📖 RBAC Library (Clean Architecture)
🚀 Overview
This library provides a Role-Based Access Control (RBAC) implementation built with Clean Architecture principles.
It is designed for enterprise-grade applications where maintainability, scalability, and developer ergonomics are critical.
Key features:
- Clean separation of Domain, Application, Infrastructure, and API layers.
- Support for Users, Roles, Permissions, Organizations, Departments, PII fields, Masking rules.
- Soft delete and audit logging via EF Core interceptors.
- Wolverine command/query bus for CQRS and event-driven workflows.
- FluentValidation for request validation.
- Rules Engine for configurable, custom business rules.

🏗 Architecture
This library follows Clean Architecture:
- Domain: Entities, value objects, exceptions, interfaces.
- Application: Commands, queries, handlers, pipeline behaviors, validators, rules.
- Infrastructure: EF Core DbContext, repositories, Unit of Work, interceptors.
- API: Controllers or endpoints consuming Application layer.

- 📂 Project Structure

```
src/
 ├── RbacService.Domain/
 │    ├── Entities/
 │    ├── Exceptions/
 │    └── Interfaces/
 ├── RbacService.Application/
 │    ├── User/
 |    |    |──Commands/
 |    |    |──CommandHandlers/
 |    |    |──Queries/
 |    |    |──QueryHandlers/
 │    ├── Roles/
 │    ├── DTOs/
 │    ├── Mappings/
 │    ├── Pipeline/
 │    ├── Validators/        
 │    └── Rules/             
 ├── RbacService.Infrastructure/
 │    ├── Data/
 │    ├── Repositories/
 │    ├── Interceptors/
 │    └── UnitOfWork/
 └── RbacService.Api/
      └── Controllers/
```


🔄 Sequence Diagram (Authorization Flow)

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant Wolverine
    participant Validator
    participant RulesEngine
    participant Handler
    participant DbContext
    participant Database

    Client->>API: Request access to resource
    API->>Wolverine: Dispatch command/query
    Wolverine->>Validator: Validate with FluentValidation
    Validator-->>Wolverine: Validation passed
    Wolverine->>RulesEngine: Evaluate custom rules
    RulesEngine-->>Wolverine: Rules passed
    Wolverine->>Handler: Execute handler
    Handler->>DbContext: Query or persist entities
    DbContext->>Database: Execute EF Core operations
    Database-->>DbContext: Return results
    DbContext-->>Handler: Provide data
    Handler-->>Wolverine: Return decision
    Wolverine-->>API: Response with result
    API-->>Client: Return 200 OK or 403 Forbidden
```

🧩 C4 Component Diagram

```mermaid
%% C4Component Diagram for RBAC Library
flowchart TD
    subgraph API_Layer["API Layer"]
        Controller["Controllers
(ASP.NET Core)
Expose endpoints for RBAC operations"]
    end

    subgraph Application_Layer["Application Layer"]
        Commands["Commands/Queries
(Wolverine)
Encapsulate use cases"]
        Handlers["Handlers
(Wolverine)
Execute business logic"]
        Validators["Validators
(FluentValidation)
Validate incoming requests"]
        Rules["Rules Engine
(RulesEngine)
Evaluate custom business rules"]
        Pipeline["Pipeline Behaviors
(Wolverine)
Inject user context, auditing"]
    end

    subgraph Domain_Layer["Domain Layer"]
        Entities["Entities
(C#)
User, Role, Permission, Organization, Department, PiiField, MaskingRule"]
        Services["Domain Services
(C#)
AccessEvaluator, MaskingService"]
        Exceptions["Exceptions
(C#)
AccessDeniedException, PiiAccessViolationException"]
    end

    subgraph Infrastructure_Layer["Infrastructure Layer"]
        DbContext["RbacDbContext
(EF Core)
Database context with relationships"]
        Repos["Repositories
(EF Core)
Concrete implementations of repository interfaces"]
        UoW["UnitOfWork
(EF Core)
Transaction boundary and repository aggregator"]
        Interceptors["Interceptors
(EF Core)
Audit & soft delete logic"]
    end

    Database["MS SQL or any database
(Persistence)"]

    Controller --> Commands
    Commands --> Handlers
    Handlers --> Validators
    Handlers --> Rules
    Handlers --> Entities
    Handlers --> Services
    Handlers --> Repos
    Repos --> UoW
    Repos --> DbContext
    DbContext --> Database
```

⚙️ Getting Started

- Install dependencies:
```
dotnet add package Wolverine
dotnet add package FluentValidation
dotnet add package RulesEngine
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

- Register services in Program.cs:
```
builder.Services.AddDbContext(connectionString);
builder.Services.AddWolverine();
builder.Services.AddRbacValidators();
builder.Services.AddRulesEngine();
```

- Run migrations:

```
dotnet ef migrations add InitialCreate -p RbacService.Infrastructure -s RbacService.Api
dotnet ef database update -p RbacService.Infrastructure -s RbacService.Api
```


🔄 Example Flow: UserCreatedCommand

Command
```
public record CreateUser(string Name, 
    string Email, 
    string? Designation, 
    Guid OrganizationId, 
    Guid ApplicationId, 
    Guid? ManagerId, 
    Guid? DepartmentId);
```

Rules Engine

Example JSON rule:
```
[
  {
    "WorkflowName": "CreateUser",
    "Rules": [
      {
        "RuleName": "DesignationRequired",
        "ErrorMessage": "Designation is required",
        "Expression": "input.Designation == null"
      }
    ]
  }
]
```

Validator:
```
namespace RbacService.Application.Validators.User
{
    public class CreateUserValidator : UserValidatorBase<Users.Commands.CreateUser>
    {
        public CreateUserValidator(IUserRepository users)
        {
            AddCommonRules(x => x.Email, x => x.Name, x => x.Designation);

            RuleFor(x => x.Email)
                .MustAsync(async (email, ct) => !await users.ExistsByEmailAsync(email, null, CancellationToken.None))
                .WithMessage("Email already exists");
        }

    }
}
```
Validator Service

```
namespace RbacService.Application.Validators
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly IValidator<T> _defaultValidator;
        private readonly RulesEngine.RulesEngine? _rulesEngine;

        public ValidatorService(IValidator<T> defaultValidator, RulesEngine.RulesEngine? rulesEngine = null)
        {
            _defaultValidator = defaultValidator;
            _rulesEngine = rulesEngine;
        }

        public async Task<IList<string>> ValidateAsync(T command, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();

            // Step 1: FluentValidation (baseline rules)
            var fluentResult = await _defaultValidator.ValidateAsync(command, cancellationToken);
            if (!fluentResult.IsValid)
                errors.AddRange(fluentResult.Errors.Select(e => e.ErrorMessage));

            // Step 2: RulesEngine (tenant-defined rules)
            if (_rulesEngine != null)
            {
                var workflowName = typeof(T).Name;
                var reResults = await _rulesEngine.ExecuteAllRulesAsync(workflowName, command);
                if (reResults != null && reResults.Any())
                {
                    foreach (var result in reResults.Where(r => !r.IsSuccess))
                    {
                        var errorMessage = result.Rule?.ErrorMessage ?? "Business rule validation failed";
                        errors.Add(errorMessage);
                    }
                }
            }

            return errors;
        }
    }
}
```

Handler (Wolverine)
```
namespace RbacService.Application.Users.CommandHandlers
{
    public class CreateUserHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<Guid> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User
            {
                UserId = Guid.NewGuid(),
                Email = command.Email,
                Name = command.Name,
                Designation = command.Designation,
                DepartmentId = command.DepartmentId,
                OrganizationId = command.OrganizationId,
                ApplicationId = command.ApplicationId,
                ManagerId = command.ManagerId
            };

            await _rbacRepository.Users.AddAsync(user, cancellationToken);
            await _rbacRepository.SaveChangesAsync(cancellationToken);
            return user.UserId;
        }
    }
}
      
```

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant Wolverine
    participant Validator
    participant RulesEngine
    participant Handler
    participant DbContext
    participant Database

    Client->>API: POST /users
    API->>Wolverine: Dispatch UserCreatedCommand
    Wolverine->>Validator: Validate with FluentValidation
    Validator-->>Wolverine: Validation passed
    Wolverine->>RulesEngine: Evaluate UserCreation rules
    RulesEngine-->>Wolverine: Rules passed
    Wolverine->>Handler: Execute UserCreatedCommandHandler
    Handler->>DbContext: Add new User entity
    DbContext->>Database: SaveChangesAsync()
    Database-->>DbContext: Persisted
    DbContext-->>Handler: User created
    Handler-->>Wolverine: Return User
    Wolverine-->>API: Success response
    API-->>Client: 201 Created + User details
```
