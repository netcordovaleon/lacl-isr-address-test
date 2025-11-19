# BigIron.RoutePlanner

## Project Structure

 ```
BigIron.RoutePlanner/
│
├── Domain/                  # Entities and Repositories (Interfaces)
│   ├── Entities/
│   └── Repositories/
│
├── Application/             # Services and DTO to the application
│   ├── Services/
│   └── DTOs/
│
├── Infraestructure/         # Specific implementations and utilities
│   ├── Repositories/
│   ├── Routing/
│   └── Services/
│
├── Web/                     # MVC Project (Razor Pages)
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   │   └── Leads/
│   └── Program.cs
│
└── README.md
 ```

## Installation

1. Clone the repository: 
```
https://github.com/netcordovaleon/lacl-isr-address-test
```

2. Navigate to the project folder and restore dependencies:
```
dotnet restore
```
3. Run the application:
```
dotnet run --project BigIron.RoutePlanner.Web
```

## Usage

- Upload a CSV file containing leads from the main page.
- Enter your home coordinates to generate an optimized route.
- Download the optimized route as a CSV file.

## Best Practices

- **Separation of Concerns:**  
  The solution is organized in layers (Domain, Application, Infrastructure, Web), following clean architecture principles for maintainability and scalability.

- **Dependency Injection:**  
  Services and repositories are registered in the DI container in `Program.cs`, enabling loose coupling and easier unit testing.

- **Use of Interfaces:**  
  Interfaces define contracts (`ILeadRepository`, `ILeadService`, etc.), allowing for flexible and interchangeable implementations.

- **Concurrency Control:**  
  The in-memory repository uses `SemaphoreSlim` to ensure thread safety for concurrent operations.

- **Model Validation:**  
  User input is validated in controllers (`ModelState.AddModelError`), and errors are displayed in views to improve user experience.

- **Async Programming:**  
  Asynchronous methods (`async/await`) are used for I/O operations, improving performance and scalability.

- **Constants for Default Values:**  
  Default values are defined as constants, avoiding magic numbers and making maintenance easier.

- **Razor Tag Helpers:**  
  Tag helpers (`asp-action`, `asp-controller`, etc.) are used for safe and type-checked route and form generation.

- **Delegation of Logic:**  
  Controllers delegate business logic and data processing to specialized services, keeping controllers focused on request/response handling.

## Technologies Used

- .NET 8
- ASP.NET Core (Razor Pages)
- C# 12
- Bootstrap (for UI)

## 🚀 About Me
Luis Cordova Leon <netcordovaleon@gmail.com>

