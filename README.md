# Edu Api

Backend API for the educational platform built with .NET using Clean Architecture.

## Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog
- Elasticsearch for logging
- MinIO for file storage

## Project Structure

- **EduApi.WebApi** - API presentation layer
- **EduApi.Application** - Business logic layer
- **EduApi.Domain** - Domain model layer
- **EduApi.Infrastructure** - Infrastructure layer (DB, external services)

## Local Development Setup

### Prerequisites

- .NET 8 SDK
- SQL Server (or SQL Server Express)

### Steps to Run Locally

1. Clone the repository:
```bash
git clone https://github.com/EduApplication/edu-api
cd EduApi
```

2. Update the connection string in `appsettings.json` or use User Secrets:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EduPortal;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

3. Run database migrations:
```bash
dotnet ef database update
```

4. Start the application:
```bash
dotnet run --project src/EduApi.WebApi
```

5. Access Swagger documentation at:
```
https://localhost:8081/swagger
```

## Docker Development

You can also run the API using Docker:

```bash
docker build -t eduapi:latest .
docker run -p 8080:80 eduapi:latest
```

## API Documentation

Detailed API documentation is available via Swagger after starting the application.

## Configuration

The application can be configured through:
- `appsettings.json` file
- Environment variables
- User Secrets (for local development)

Key configuration sections:
- `ConnectionStrings` - Database connection
- `Jwt` - Authentication settings
- `Cors` - Cross-Origin Resource Sharing
- `Serilog` - Logging configuration
- `ElasticConfiguration` - Elasticsearch settings
- `Minio` - File storage settings
