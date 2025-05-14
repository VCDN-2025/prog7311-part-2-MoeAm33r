Agri-Energy Connect
Bridging Agriculture and Green Energy in South Africa

Project Banner
Demo Video: Watch on YouTube
📌 Table of Contents

    Features

    Tech Stack

    Installation

    Configuration

    Database Setup

    Usage

    API Endpoints

    Screenshots

    References

    License

🌟 Features

Role-Based Access Control:

    👨‍🌾 Farmers: Add products, track production dates, manage profiles

    👨‍💼 Employees: Manage farmers, filter products, generate reports

Core Modules:

    🛒 Product Marketplace (Solar pumps, bioenergy tech)

    📚 Sustainable Farming Resource Hub

    📊 Real-time Data Dashboard

Security:

    🔒 ASP.NET Core Identity with JWT

    🛡️ CSRF/XSS protection

🛠️ Tech Stack
Component	Technology
Backend	ASP.NET Core 8.0 MVC
Database	SQL Server + Entity Framework Core 8
Frontend	Razor Pages, Bootstrap 5.3
Authentication	Identity + Role Management
Deployment	Docker-ready
💻 Installation
Prerequisites:

    .NET 8.0 SDK

    Visual Studio 2022 (or VS Code)

    SQL Server 2022

Steps:

    Clone the repo:
    bash

git clone https://github.com/MoeAm33r/AgriEnergyConnect.git
cd AgriEnergyConnect

Restore packages:
bash

    dotnet restore

    Configure the database (see Database Setup).

⚙️ Configuration

Edit appsettings.json:
json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyConnect;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "YourSecureKeyHere",
    "Issuer": "https://agrienergyconnect.com"
  }
}

🗃️ Database Setup

    Run migrations:
    bash

    dotnet ef migrations add InitialCreate
    dotnet ef database update

    Default Admin Account:

        Email: admin@agrienergy.com

        Password: Admin@123

    Schema:
    Database Schema

🚀 Usage

    Start the app:
    bash

    dotnet run

        Access: https://localhost:5001

    User Flows:

        Farmers: /Farmer/Dashboard

        Employees: /Employee/Dashboard

    Demo Video:
    YouTube Demo

🌐 API Endpoints
Endpoint	Method	Description
/api/products	GET	List all products
/api/farmers	POST	Register new farmer
/api/auth/login	POST	JWT authentication
📸 Screenshots
Login Page
![image](https://github.com/user-attachments/assets/3824eea6-478a-4f53-9a7c-93a172547467)

Farmer	Dashboard
![image](https://github.com/user-attachments/assets/33d6d45c-f871-4458-8ba3-5d6bf611face)

📚 References

    Microsoft (2024) ASP.NET Core Identity. [Online] Available at: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity

    Bootstrap Team (2024) Bootstrap 5 Documentation. [Online] Available at: https://getbootstrap.com/docs/5.3/

