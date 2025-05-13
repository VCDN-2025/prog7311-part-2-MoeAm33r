# Agri-Energy Connect

![Project Banner](https://via.placeholder.com/800x200.png?text=Agri-Energy+Connect)

A web platform connecting farmers with green energy solutions in South Africa. Built with ASP.NET Core MVC.

## Features

- **Role-based authentication** (Farmers & Employees)
- **Farmer Dashboard**:
  - Add/manage agricultural products
  - Track production dates
- **Employee Portal**:
  - Manage farmer profiles
  - Filter products by category/date
- **Responsive Design** works on all devices

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (or VS Code)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/MoeAm33r/AgriEnergyConnect.git
   cd AgriEnergyConnect
Configuration

Edit appsettings.json for your database connection:
json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyConnect;Trusted_Connection=True;"
  }
}

Running the Application
bash

dotnet run

Access the app at: https://localhost:5001
Default Accounts
Role	Email	Password
Admin	admin@agrienergy.com	Admin@123
Farmer	(Register new account)	
Project Structure

AgriEnergyConnect/
├── Controllers/     # MVC controllers
├── Data/            # Database context
├── Models/          # Entity models
├── Views/           # Razor views
├── wwwroot/         # Static files
├── Program.cs       # Startup
└── appsettings.json # Configuration
