# Ballast Lane Portal API

## User Story:

As a frontend developer, I would like to have a user-authenticated API to manage the company's applications on the new Ballast Lane portal. This API should handle all CRUD requests, along with a paginated endpoint to retrieve the applications. Also, only admin users are allowed to perform write operations, while regular users are only allowed to perform read operations.

## Architecture Decisions:

The API is developed using clean architecture concepts, DDD, and TDD for testing. An additional project named 'IoC' is added to manage all dependency injections and make the API project less dependent on other solution projects.

The focus of the project is on CRUD operations for application and user data, managing permissions, and showcasing the techniques used to perform these operations. Some additional data validations would be required in case this application is deployed and used for other services.

- The API project contains all endpoints and handles authorization and authentication management.
- The Application Service project contains all business rules.
- The Domain project shares all data classes.
- The Infrastructure.Data project is responsible for CRUD operations on MongoDB.
- The IoC project is responsible for managing dependency injection.

## Execution Instructions:

The Ballast Lane application is a Web API developed in .NET 8, with MongoDB as the database. The application and the database are running in Docker containers, and a docker-compose file manages the creation of the database along with the migration data. The project './client/docker-compose' contains all configurations.

Use Visual Studio with .NET 8 installed to execute the application. The computer needs to have Docker Desktop installed as well.

After running the application, use Swagger to authenticate and use the Application and User endpoints.

To authenticate with admin privileges (read and write), use the user 'admin:Test123!'.

To authenticate with read privileges, use the user 'john:Test123!'. If this user tries to execute any POST, PUT, or DELETE operation, a forbidden response is expected.

The application should not require any further steps to be executed and tested.
