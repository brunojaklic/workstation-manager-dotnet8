# WorkstationManager

A desktop application built with **.NET 8**, **Avalonia UI**, **Entity Framework**, and **CommunityToolkit.MVVM**. This application allows administrators to manage user work assignments and roles, while providing users with access to their current work details.

---

## ✨ Features

### 🔐 Login System
- Users authenticate using their **username** and **password**.
- Passwords are securely hashed using **BCrypt**.

### 👨‍💼 Admin Panel
- View all users and their assigned work positions with timestamps.
- Assign or change work positions for users.
- Create new users with role and work position assignment.

### 👤 User Panel
- View your full name and the **currently assigned workstation**.

---

## 🗃️ Database Schema

Entity Framework is used to manage a local **MySQL** database with the following tables:

### `users`
| Column      | Type               |
|-------------|--------------------|
| id          | int (PK)           |
| first_name  | string             |
| last_name   | string             |
| username    | string             |
| password    | string (hashed)    |
| role_id     | FK → roles.id      |

### `roles`
| Column      | Type                   |
|-------------|------------------------|
| id          | int (PK)               |
| name        | string (Admin/User)    |
| description | string                 |

### `work_positions`
| Column      | Type                   |
|-------------|------------------------|
| id          | int (PK)               |
| name        | string                 |
| description | string                 |

### `user_work_positions`
| Column          | Type                   |
|-----------------|------------------------|
| id              | int (PK)               |
| user_id         | FK → users.id          |
| work_position_id| FK → work_positions.id |
| product_name    | string                 |
| date_assigned   | DateTime               |

---

## 🏗️ Technologies Used

- **.NET 8**
- **Avalonia UI** – Cross-platform UI framework
- **Entity Framework Core** – ORM for database interaction
- **MySQL** – Relational database
- **Docker** – Containerization for running services and dependencies
- **CommunityToolkit.MVVM** – Lightweight MVVM support
- **BCrypt.Net** – Password hashing for secure storage

---

## 🧱 Architecture

The application follows a clean **MVVM architecture**.

---

## 🚀 How to Run

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (required to run Docker containers)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Steps to Run

1. Open a terminal and type:

   ```bash
   cd .\WorkstationManager\
   docker compose up -d
   dotnet clean
   dotnet build
   dotnet ef database update
   dotnet run
   
