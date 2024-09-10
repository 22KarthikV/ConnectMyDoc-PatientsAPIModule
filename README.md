# CMD (Connect My Doc) - Patient Module

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Database](#database)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [Branching Strategy](#branching-strategy)
- [License](#license)
- [Contact](#contact)

## Introduction

CMD (Connect My Doc) is a comprehensive healthcare management system. This repository contains the Patient Module, which is responsible for managing patient information, appointments, and health records.

## Features

- Patient registration and profile management
- Appointment scheduling and management
- Health condition tracking
- Primary clinic and doctor assignment
- Role-based access control (Admin, Doctor, User)

## Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger/OpenAPI

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or Visual Studio Code

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/cmd-patient-module.git
    ```

2. Navigate to the project directory:
    ```bash
    cd cmd-patient-module
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Update the connection string in `appsettings.json` to point to your SQL Server instance.

5. Apply database migrations:
    ```bash
    dotnet ef database update
    ```

6. Run the application:
    ```bash
    dotnet run
    ```

## Usage

After starting the application, you can access the Swagger UI at [https://localhost:5001/swagger](https://localhost:5001/swagger) to interact with the API endpoints.

## Database

The project uses Entity Framework Core with SQL Server. The database schema is managed through migrations.

- To add a new migration:
    ```bash
    dotnet ef migrations add MigrationName
    ```

- To update the database:
    ```bash
    dotnet ef database update
    ```

## Deployment

For deployment instructions, please refer to `DEPLOYMENT.md`.

## Contributing

We welcome contributions to the CMD Patient Module. Please read `CONTRIBUTING.md` for details on our code of conduct and the process for submitting pull requests.

## Branching Strategy

We use a feature branch workflow. Here are the main commands for working with branches:

1. **Create a new feature branch:**
    ```bash
    git checkout -b feature/your-feature-name
    ```

2. **Make your changes and commit them:**
    ```bash
    git add .
    git commit -m "Description of your changes"
    ```

3. **Push your branch to GitHub:**
    ```bash
    git push origin feature/your-feature-name
    ```

4. **Create a Pull Request on GitHub from your feature branch to the main branch.**

5. **After approval, merge the Pull Request on GitHub.**

6. **Update your local main branch:**
    ```bash
    git checkout main
    git pull origin main
    ```

7. **Delete the feature branch locally and remotely:**
    ```bash
    git branch -d feature/your-feature-name
    git push origin --delete feature/your-feature-name
    ```

## License

This project is licensed under the MIT License - see the `LICENSE.md` file for details.

## Contact

For any queries or support, please contact [karthik](mailto:karthikmudaliar20@gmail.com).

