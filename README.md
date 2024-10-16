# Blog Solution Clean Architecture

## Overview
### Blog Solution is a web application designed using Clean Architecture principles. It serves as a robust platform for creating, managing, and publishing blog posts. The architecture separates concerns and promotes maintainability, testability, and scalability.

## Features
* User Authentication and Authorization
* CRUD Operations for Blog Posts
* Categories and Tags Management
* Responsive Design
* RESTful API for Integration
* Unit and Integration Testing

## Architecture
### This project follows the Clean Architecture approach, ensuring that the codebase remains organized and scalable. The architecture layers include:

* Domain Layer: Contains business logic and entities.
* Application Layer: Handles application-specific logic and orchestrates operations.
* Infrastructure Layer: Deals with external services, databases, and APIs.
* Presentation Layer: Responsible for the user interface and user experience.
## Technologies Used
* ASP.NET Core
* Entity Framework Core
* SQL Server
* AutoMapper
* Swagger for API Documentation
* Unit Testing with xUnit
## Getting Started
### Prerequisites
Before you begin, ensure you have the following installed on your machine:

* .NET SDK 8.0
* SQL Server
* Visual Studio or any IDE of your choice
### Installation
1. Clone the repository:
```
git clone https://github.com/MounirSamyAziz/BlogSolutionCleanArchitecture.git
```
2. Navigate to the project directory:


```
cd BlogSolutionCleanArchitecture
```
3. Restore the NuGet packages:

```
dotnet restore
```
4. Set up the database (if using SQL Server):

** Update the connection string in the appsettings.json file.
** Run the migrations:
```
dotnet ef database update
```
5. Run the application:

```
dotnet run
```
6. Open your browser and navigate to http://localhost:5000 to access the application.

## API Documentation
### The API documentation can be accessed via Swagger UI once the application is running. Visit http://localhost:5000/swagger to explore the available endpoints.
