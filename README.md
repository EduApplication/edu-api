# edu-api

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