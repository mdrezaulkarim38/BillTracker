# Bill Tracker

Bill Tracker is a web application designed to help users manage and track their bills efficiently. The application features user authentication, role-based access, and an intuitive user interface for managing bills and tracking payment statuses.

## Features

- **User Authentication**: Secure login and logout functionality using session cookies.
- **Role-Based Access Control**: Different dashboards and functionalities for Admin and User roles.
- **Bill Management**: Add, update,final amount submit, and track bills.
- **Password Reset**: Allows users to reset their passwords securely.
- **Responsive UI**: Built with Bootstrap to ensure compatibility with different devices.
- **Audit Logging**: Track user activities such as login attempts and password resets.

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Views with Bootstrap
- **Database**: SQL Server
- **Authentication**: Cookie-based authentication
- **Logging**: Built-in ASP.NET Core logging
- **Dependency Injection**: For modular and testable components

## Project Structure

```
BillTracker
├── Controllers
│   ├── AuthController.cs
│   ├── AdminController.cs
│   ├── UserController.cs
│   └── HomeController.cs
├── Data
│   ├── script.sql
│   └── ApplicationDbContext.cs
├── Interfaces
│   ├── IAdminService.cs
│   ├── IAuthService.cs
│   └── IUserService.cs
├── Migrations
│   └── All the Files
├── Models
│   ├── ViewModels
│   │   ├── LoginViewModel.cs
│   │   ├── EditUserViewModel.cs
│   │   └── AddUserViewModel.cs
│   ├── ErrorViewModel.cs
│   ├── ProductModel.cs
│   └── User.cs
├── Services
│   ├── AdminService.cs
│   ├── BaseService.cs
│   ├── UserService.cs
│   └── AuthService.cs
├── Views
│   ├── Admin
│   │   ├── AddUser.cshtml
│   │   ├── EditUser.cshtml
│   │   ├── UserManage.cshtml
│   │   └── Dashboard.cshtml
│   ├── User
│   │   ├── ProductEntry.cshtml
│   │   └── Dashboard.cshtml
│   ├── Auth
│   │   ├── Login.cshtml
│   │   └── ResetPassword.cshtml
│   └── Shared
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot
│   ├── css
│   ├── images
│   ├── js
│   ├── libs
│   └── scss
├── appsettings.json
├── Program.cs
└── Startup.cs
```

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/<your-username>/bill-tracker.git
   cd bill-tracker
   ```

2. **Set Up the Database**:
   - Create a new SQL Server database.
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings" : {
         "DefaultConnection": "Server=your_server;Database=your_database;Trusted_Connection=True;"
     }
     ```

3. **Run Migrations**:
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

5. **Access the Application**:
   Open your browser and navigate to `http://localhost:5000`.

## Usage

### Authentication
- Admins and users can log in using their credentials.
- Reset password functionality is available for authenticated users.

### Admin Role
- Manage users (activate, deactivate, assign roles).
- Access an admin dashboard.

### User Role
- View personal dashboard.
- Add and track bills.

## Contribution

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Make your changes and commit them:
   ```bash
   git commit -m "Add your commit message"
   ```
4. Push the changes to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Create a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---

**Developed by MD Rezaul Karim**
