# Person Example

## Prerequisites 

### Installs

- .NET 10
- PostgreSQL (running locally on port 5432)

### Database Setup

1. **Install EF Core CLI** (one-time only):
```powershell
dotnet tool install --global dotnet-ef
```

2. **Create migration**:
```powershell
dotnet ef migrations add InitialCreate --project "PersonExample\PersonExample.csproj"
```

3. **Apply migration to database**:
```powershell
dotnet ef database update --project "PersonExample\PersonExample.csproj"
```

Via Program.cs on startup it will automatically:
- Checks for pending migrations
- Creates the database if it doesn't exist
- Creates all tables
- No manual steps needed after initial setup!

## Run/Debug Application

### Local Development

- Use `https` or `http` launch configs
- Browser will automatically open to Swagger UI (/swagger)
- Access: https://localhost:7009/swagger

## TODO

- DTO Mapping (Mapster)
- seggregation of concerns (services, repositories, controllers,...)
- Validation
- Auth
- ´Database versioning (e.g. changed column name, new column,...)´
- Global exception handling / correct exceptions in app



# Remarks

## Authentication 

### 1. Service and Database Setup
- ApplicationUser.cs:
	- `ApplicationUser` — your user entity (extends IdentityUser which has Id, Email, PasswordHash, UserName, etc.)
- PersonDbContext.cs:
	- `PersonDbContext : IdentityDbContext` — tells EF to include Identity tables in migrations
- Program.cs:
	- `AddIdentity<ApplicationUser, IdentityRole>()` — registers UserManager and RoleManager as injectable services
	- `UseAuthentication()` — middleware that reads the incoming token and populates HttpContext.User
- Update Database 
	- `dotnet ef migrations add AddIdentity --project "PersonExample\PersonExample.csproj"`
	- `dotnet ef database update --project "PersonExample\PersonExample.csproj"`