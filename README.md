# Inventory Management API (Prueba Redarbor)

RESTful API for inventory management built with .NET 8 following Clean Architecture, CQRS, SOLID principles and Clean Code practices.

## Table of Contents

- Technologies
- Architecture
- Features
- Requirements
- Run Application Locally
- Swagger
- JWT Authentication
- Error Handling
- Run Tests
- Run With Docker
- Design Patterns and Practices
- Business Rules
- Notes
- Author

## Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Dapper
- MediatR
- FluentValidation
- SQL Server
- Docker
- Swagger
- JWT Authentication
- xUnit
- Moq

---

## Architecture

The solution follows Clean Architecture principles with CQRS pattern implementation.

```txt
Client
   |
   v
Inventory.Api
   |
   v
Inventory.Application
   |
   v
Inventory.Domain
   |
   v
Inventory.Infrastructure
   |
   v
SQL Server
```

## Layers

- Inventory.Api
- Inventory.Application
- Inventory.Domain
- Inventory.Infrastructure
- Inventory.Tests

---

## Features

- Product CRUD (Soft Delete support)
- Category CRUD
- Inventory movements
- Rich domain model for inventory stock management
- Transactional inventory operations
- JWT Authentication
- Global exception handling
- Validation pipeline with FluentValidation
- Swagger documentation
- Docker support
- Unit testing
- Structured application logging

---

## Requirements

### Option 1 - Run Locally

- .NET 8 SDK
- SQL Server

### Option 2 - Run With Docker

- Docker Desktop

---

## Run Application Locally

### 1. Clone repository

```bash
git clone https://github.com/bruno-clavijo/InventoryManagement.git
```

### 2. Navigate to project

```bash
cd InventoryManagement
```

### 3. Update connection string

Edit:

```txt
src/Inventory.Api/appsettings.json
```

### Local SQL Server Example

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=InventoryDb;User Id=sa;Password=YourPassword123*;TrustServerCertificate=True"
}
```

### 4. Restore packages

```bash
dotnet restore
```

### 5. Apply migrations

```bash
dotnet ef database update --project ./src/Inventory.Infrastructure --startup-project ./src/Inventory.Api
```

### 6. Run API

```bash
dotnet run --project ./src/Inventory.Api
```

---

## Swagger

Swagger UI:

```txt
http://localhost:5098/swagger
```

---

## JWT Authentication

### Login Endpoint

```txt
POST /api/auth/login
```

v

Use returned JWT token in Swagger Authorize button.

---

## Error Handling

The API uses centralized exception handling middleware.

### HTTP Status Codes

| Status | Description             |
| ------ | ----------------------- |
| 200    | Successful operation    |
| 201    | Resource created        |
| 400    | Validation errors       |
| 401    | Unauthorized            |
| 404    | Resource not found      |
| 422    | Business rule violation |
| 500    | Unexpected server error |

---

## Run Tests

Unit tests cover:

- Product validation rules
- Inventory movement creation
- Product not found scenarios
- Insufficient stock scenarios
- Successful inventory transactions

````bash
dotnet test

---

## Run With Docker

### 1. Clone repository

```bash
git clone https://github.com/bruno-clavijo/InventoryManagement.git
````

### 2. Navigate to project

```bash
cd InventoryManagement
```

### 3. Build and run containers

```bash
docker compose up --build
```

---

### 4. Docker Swagger URL

```txt
http://localhost:8080/swagger
```

---

### Docker SQL Server Connection

### Server

```txt
localhost,1434
```

### Authentication

SQL Server Authentication

### User

```txt
sa
```

### Password

```txt
InventoryDbPassword123*
```

### Docker SQL Server Example

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=inventory.sqlserver;Database=InventoryDb;User Id=sa;Password=InventoryDbPassword123*;TrustServerCertificate=True"
}
```

---

## Design Patterns and Practices

- Clean Architecture
- CQRS
- Repository Pattern
- Dependency Injection
- SOLID Principles
- Clean Code
- Rich Domain Model
- Domain Exceptions
- Global Exception Handling
- Validation Pipeline Behavior
- Transaction Management
- Soft Delete Pattern

---

## Business Rules

### Inventory Movements

- Entry movements increase product stock.
- Exit movements decrease product stock.
- Exit movements cannot exceed available stock.
- Quantities must be greater than zero.

### Product Management

- Products are soft deleted using the IsActive flag.
- Inactive products are excluded from application queries.

---

## Notes

When running with Docker, the connection string is automatically configured through docker-compose.yml environment variables.

## Author

Bruno Fernandez Clavijo
