# SkateRentPlannerBackend

## 📋 Overview
This is the backend for the Skate Rent and Event Planner app, built with ASP.NET Core.

## 🚀 Features
- ✅ Skate rental (bookings) management
- 📅 Event scheduling and multi-user booking
- 🔐 User authentication & authorization
- 🌐 RESTful API with full Swagger documentation
- 💾 Database integration using Entity Framework Core

## 🔧 Technologies
- **.NET 8.0.101**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server / MySQL (Pomelo)**
- **BCrypt.Net-Next** (for secure password hashing)

## 💻 Setup Instructions
1. **Clone the repository: git clone https://github.com/Margare-ta/SkateEventManager**
2. **In the terminal: cd SkateEventManager** 
3. **Download ef if needed (dotnet tool install --global dotnet-ef)**
4. **Create/Update database: (dotnet ef database update)**
5. **Run Project** 
6. **Program launches on: "http://localhost:3000"**
7. **Swagger: http://localhost:3000/swagger/index.html**

## Important notes:
Rents is defind as Book here!
1 user can book for multiple events. 
