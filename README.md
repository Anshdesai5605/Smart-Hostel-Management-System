# Smart Hostel Management System

A modern, full-featured web application built using ASP.NET Core 8.0 MVC. It provides a complete solution for managing a student hostel, from room allocation and complaints to mess menu planning. The system features two distinct roles: **Student** and **Admin (Warden)**, each with a custom dashboard and specific permissions.

Here is a showcase of the application's user interface and core features.

### 1. Admin Dashboard
The central hub for the admin/warden to manage all hostel operations.
![Admin Dashboard](./assets/admin_panel.png)

### 2. Secure Login Page
A clean, modern, and centered login form.
![Login Page](./assets/login_screen.png)

### 3. Student Dashboard
A dashboard for students to checkout Mess menu, file compaints, and view complaint's status.
![Interactive Data Table](./assets/student_screen.png)
---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 8.0 (MVC with Razor Views)
* **Language:** C#
* **Database:** SQLite (via Entity Framework Core 8)
* **Frontend:** Bootstrap 5, HTML, CSS, JavaScript
* **Core Features:**
    * **Authentication:** Session-based authentication
    * **Database Management:** Entity Framework Core Migrations
    * **UI:** Modern card-based design with interactive tables (DataTables.js) and icons (Font Awesome)
    * **Performance:** In-Memory Caching

---

## ✨ Features

### 🧑‍🎓 Student Features
* **Secure Login:** Students can log in to their personal account.
* **Student Dashboard:** View today's mess menu at a glance and access key actions.
* **Complaint Management:** Students can create new complaints and view the status of their past complaints.
* **View Mess Menu:** Access the full mess menu list.

### 👮 Admin (Warden) Features
* **Secure Login:** Admin has a separate account with elevated privileges.
* **Admin Dashboard:** A central hub to manage all hostel operations.
* **Room Management (CRUD):** Add, update, view, and delete hostel rooms and their capacity.
* **Mess Menu Management (CRUD):** Create, update, and delete the daily/weekly mess menu.
* **Complaint Resolution (CRUD):** View all student complaints, update their status (e.g., "Pending" to "Resolved"), and delete them.

---

## 🚀 Getting Started & How to Run

This project is built to be self-contained and easy to run. It uses SQLite, so no external database server is required.

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* A code editor (like Visual Studio Code)
* A terminal (like PowerShell or Command Prompt)

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/](https://github.com/)<your-username>/SmartHostelManagementSystem.git
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd SmartHostelManagementSystem
    ```

3.  **Restore dependencies:**
    This command downloads all the necessary packages (like ASP.NET Core, EF Core, etc.).
    ```bash
    dotnet restore
    ```

4.  **Create the Database:**
    The project is configured to use EF Core Migrations to automatically build the SQLite database (`hostel.db`) and populate it with seed data.
    ```bash
    dotnet ef database update
    ```
    *(If you get a `dotnet-ef` error, install the tool with: `dotnet tool install --global dotnet-ef`)*

5.  **Run the project!**
    ```bash
    dotnet run
    ```

6.  **Access the application:**
    Open your web browser and navigate to the URL shown in the terminal (usually `https://localhost:7xxx` or `http://localhost:5xxx`).

---

## 🔑 Default Logins

The database is pre-seeded with two user accounts for testing:

* **Admin / Warden:**
    * **Email:** `admin@hostel.com`
    * **Password:** `admin`

* **Student:**
    * **Email:** `john@student.com`
    * **Password:** `password`

---

## 🏛️ Architecture & Key Concepts

This application demonstrates several key concepts of modern web development:

### 1. Database (SQLite & EF Core)
* **What:** The project uses **SQLite**, a serverless, file-based database, which is perfect for development as it's just a single file (`hostel.db`) in the project.
* **How:** **Entity Framework Core (EF Core)** is used as the ORM (Object-Relational Mapper). This allows us to interact with the database using simple C# models (like `Student.cs`, `Room.cs`) instead of writing raw SQL.

### 2. Session State (Web "Shared Preferences")
* **What:** In ASP.NET Core, the equivalent concept to "Shared Preferences" for storing user-specific data is **Session State**.
* **How:** When a user logs in, their `UserId`, `UserName`, and `IsAdmin` status are saved in a temporary session. This "session cookie" is sent with every request, allowing the application to remember who the user is, show the correct navigation links, and secure admin-only pages.

### 3. Cache Memory (`IMemoryCache`)
* **What:** This is a high-speed, temporary storage location in the server's memory.
* **How:** To improve performance, the **"Today's Mess Menu"** on the dashboard is cached. The first time anyone loads the page, the menu is fetched from the database and saved in the cache. For all other users on the same day, the menu is served instantly from the cache, reducing database load and making the app feel faster.

---

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.