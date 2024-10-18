# Energy Market Balancing - Backend

## Overview

The **Energy Market Balancing** backend application is responsible for managing balancing circles, their members, and forecasted inflows and outflows in the energy market. It provides the necessary endpoints to retrieve balancing circle data and forecasted energy consumption and production, enabling calculations for imbalances in energy flow.

### Context

Balancing is critical for maintaining grid stability at 50 Hz within the energy market. Each participant is assigned to a "balancing circle," which tracks total inflows (energy produced) and outflows (energy consumed). Imbalances occur when the sum of inflows does not equal the sum of outflows, and they are defined as follows:

**Imbalance(t) = Inflows(t) - Outflows(t)**

- **Inflows**: Energy produced by sources like solar farms and wind parks.
- **Outflows**: Energy consumed by clients or industrial entities.

The primary goal is to minimize imbalances and maintain cost efficiency.

## Features

- Retrieve a list of balancing circles and their members.
- Calculate day-ahead imbalances based on forecasted inflows and outflows.
- Provide detailed member-level breakdowns for imbalance data.
- Unit tests for ensuring functionality and reliability.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed on your machine:

- .NET 6.0 SDK or later
- SQL Server or any compatible database

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/energy-market-balancing-backend.git
   cd energy-market-balancing-backend

### API Endpoints
The backend provides several API endpoints:

- GET /api/v1/balancing: Retrieve all balancing circles with their members.
- GET /api/v1/balancing/member/{id}/forecast: Retrieve forecast data for a specific member.
- GET /api/v1/balancing/{id}/imbalances: Calculate and retrieve imbalances for a specific balancing circle.
  
### Architectural Decisions
- **Entity Framework Core**: Used for database interactions, allowing for an ORM-based approach to manage the database schema and migrations.
- **Dependency Injection**: Utilized to manage dependencies, enhancing modularity and testability.
- **Service Layer**: Implemented to handle business logic, keeping the controllers slim and focused on handling HTTP requests and responses.
- **Logging**: Integrated to monitor application behavior and facilitate debugging.

## Future Improvements

- **Caching**: Implement caching for frequently accessed data to improve performance.
- **Error Handling**: Enhance error handling to provide more user-friendly messages and ensure robustness.
- **Unit Tests**: Expand unit test coverage for all critical components to ensure reliability.
- **Microservices Architecture**: Consider refactoring the application to a microservices architecture for improved scalability and maintainability.
  
## Screenshots
SQL Management Studio
![sqldatabase](https://github.com/user-attachments/assets/cf228ade-9931-4c35-9240-bfb4e584363e)


## Testing Results
- dotnet test
![tests](https://github.com/user-attachments/assets/5b149dda-85b9-459c-ab96-13891aaa88c6)
