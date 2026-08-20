# Task Management System

A small ASP.NET Core Web API designed to help students organize and manage their academic tasks efficiently.

The system allows students to create, update, delete, and track their academic tasks, including deadlines and completion status.

---

## Project Overview

The **Student Task Management System** is a RESTful Web API built with ASP.NET Core.

The main goal of the project is to provide students with a simple and structured system for managing their personal academic tasks.

The project follows a **3-Tier Layered Architecture** to separate responsibilities between the API, Business Logic, and Data Access layers.

---

## Objectives

* Provide students with an organized way to manage their academic tasks.
* Allow students to track task deadlines and completion status.
* Enable users to create, update, and delete tasks.
* Provide a simple and structured system for managing personal tasks.
* Implement a structured RESTful Web API using ASP.NET Core.
* Apply the 3-Tier Layered Architecture Pattern.
* Separate responsibilities to make the code easier to maintain and extend.

---

## Main Features

### 1. User Authentication

The system provides:

* User Registration
* User Login
* JWT Authentication
* User-based access to personal tasks
* Password validation using ASP.NET Core Identity

Each authenticated user can only access their own tasks.

---

### 2. Task Management

Authenticated students can:

* Create new tasks
* View all their tasks
* View a specific task
* Update existing tasks
* Delete tasks
* Add or modify task deadlines
* Mark tasks as completed
* Mark completed tasks as pending again

---

### 3. Task Filtering

Users can filter their tasks according to their completion status.

Supported statuses:

* `Pending`
* `Completed`

## Architecture

The project follows a **3-Tier Layered Architecture**

---
##  Project Structure

```text
TaskManagementSystem
│
├── src
│   │
│   ├── TaskManagementSystem.Api
│   │   ├── Controllers
│   │   │   ├── AuthController.cs
│   │   │   └── TasksController.cs
│   │   │
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── Properties
│   │
│   ├── TaskManagementSystem.BLL
│   │   ├── DTOs
│   │   │   ├── AuthResponse.cs
│   │   │   ├── LoginRequest.cs
│   │   │   ├── RegisterRequest.cs
│   │   │   ├── TaskCreateDto.cs
│   │   │   ├── TaskUpdateDto.cs
│   │   │   └── TaskResponseDto.cs
│   │   │
│   │   ├── Interfaces
│   │   ├── Models
│   │   └── Services
│   │
│   └── TaskManagementSystem.DAL
│       ├── Common
│       ├── DbContext
│       ├── Entities
│       ├── Migrations
│       └── Repositories
│
├── Requests
│   └── requests.http
│
├── TaskManagementSystem.slnx
└── README.md
```

##  How to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/ahmed-hossam-moka/TaskManagementSystem
```

### 2. Open the Project

Open:

```text
TaskManagementSystem.slnx
```

### 3. Configure SQL Server

Make sure SQL Server / LocalDB is available.

The default connection string is configured in:

```text
src/TaskManagementSystem.Api/appsettings.json
```

Example:

```text
Server=(localdb)\MSSQLLocalDB;
Database=TMS;
Trusted_Connection=True;
MultipleActiveResultSets=true
```

### 4. Apply Database Migrations

Run:

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run --project src/TaskManagementSystem.Api
```

The application runs using the configured HTTP/HTTPS URLs.
