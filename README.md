## ECommerceAPI

A RESTful E-Commerce API built with ASP.NET Core 8 following Clean Architecture and SOLID Principles.

## Architecture

This project follows **Clean Architecture** with 4 layers:

- **Domain** - Entities and Repository Interfaces
- **Core** - Business Logic, Services, DTOs, Validators
- **Infrastructure** - DbContext, Repository Implementations
- **UI** - Controllers, Filters, Middleware

## SOLID Principles

This project follows SOLID principles:

- Single Responsibility - Each class has one responsibility (Repository, Service, Controller are separated)
- Open/Closed - Open for extension, closed for modification (Generic Repository, Interface-based design)
- Liskov Substitution - Concrete implementations can replace their interfaces anywhere
- Interface Segregation - Separate interfaces for each entity (IProductRepository, IOrderRepository, etc.)
- Dependency Inversion - Controllers depend on abstractions (IProductService) not concrete classes (ProductService)

## Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation
- Swagger / OpenAPI
- BCrypt Password Hashing

## Features

- JWT based Authentication & Authorization
- Role based access control (Admin / Customer)
- Product management with category support
- Shopping cart system
- Order management
- Address management
- Pagination and search
- Global exception handling
- FluentValidation for input validation

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/ECommerceAPI.git
```

2. Update connection string in `appsettings.json`
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

3. Run migrations
```bash
dotnet ef database update
```

4. Run the project
```bash
dotnet run
```

5. Open Swagger UI
```
https://localhost:7023/swagger
```

## Authentication

Register and login to get a JWT token:
```
POST /api/User/register
POST /api/User/login
```

Use the token in Swagger's **Authorize** button:
```
Bearer {your_token}
```

## API Endpoints

All endpoints are documented and testable via Swagger UI:
```
https://localhost:7023/swagger
```
