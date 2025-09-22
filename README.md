# TaskTracker API

A RESTful API for managing tasks and to-do items, built with .NET 9.0 and deployed on Azure App Service.

## Features

- **Full CRUD operations** for task management
- **Azure Table Storage** for data persistence
- **Swagger/OpenAPI** documentation
- **Automated CI/CD** with GitHub Actions
- **Containerized** with Docker support

## Live Demo

🌐 **API URL**: https://web-tasktracker-16604.azurewebsites.net
📋 **Swagger UI**: https://web-tasktracker-16604.azurewebsites.net/swagger

## API Endpoints

- `GET /api/tasks` - Get all tasks
- `POST /api/tasks` - Create a new task
- `GET /api/tasks/{id}` - Get a specific task
- `PUT /api/tasks/{id}` - Update a task
- `DELETE /api/tasks/{id}` - Delete a task

## Tech Stack

- **.NET 9.0** - Web API framework
- **Azure Table Storage** - Database
- **Azure App Service** - Hosting platform
- **GitHub Actions** - CI/CD pipeline
- **Docker** - Containerization
- **Swagger** - API documentation

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Azure Storage Account (for local development)

### Running Locally
