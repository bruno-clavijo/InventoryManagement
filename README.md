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
- Run Tests
- Run With Docker
- Design Patterns and Practices
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

- Product CRUD
- Category CRUD
- Inventory movements
- JWT Authentication
- Global exception handling
- Validation pipeline
- Swagger documentation
- Docker support
- Unit testing

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

### Docker SQL Server Example

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=inventory.sqlserver;Database=InventoryDb;User Id=sa;Password=InventoryDbPassword123*;TrustServerCertificate=True"
}
```

### 4. Apply migrations

```bash
dotnet ef database update \
--project ./src/Inventory.Infrastructure \
--startup-project ./src/Inventory.Api
```

---

### 5. Run API

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

### Credentials

```json
{
  "username": "admin",
  "password": "Admin123*"
}
```

Use returned JWT token in Swagger Authorize button.

---

## Run Tests

```bash
dotnet test
```

---

## Run With Docker

### Build and run containers

```bash
docker compose up --build
```

---

### Docker Swagger URL

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

---

## Design Patterns and Practices

- Clean Architecture
- CQRS
- Repository Pattern
- Dependency Injection
- SOLID Principles
- Clean Code
- Global Exception Handling
- Validation Pipeline Behavior

---

## Notes

When running with Docker, the connection string is automatically configured through docker-compose.yml environment variables.

## Author

Bruno Fernandez Clavijo
