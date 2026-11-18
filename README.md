 GradePilot

GradePilot is a GPA and CGPA calculation web application developed for Astana IT University (AITU) students.
It helps students manage their semesters, record course grades, calculate semester GPA, and track their cumulative GPA using the AITU grading scale.

Features

- Semester Management: Create and delete academic semesters.
- Course Management: Add and remove courses within each semester.
- Grade Calculation: Calculate final course percentages using the AITU assessment weights:
  - Midterm: 30%
  - Endterm: 30%
  - Final Exam: 40%
- AITU Grading Scale: Convert final percentages into AITU letter grades and grade points.
- Semester GPA: Calculate GPA for each semester based on course grades and credit hours.
- Cumulative GPA (CGPA): Calculate the overall GPA across all semesters, shown on the My Semesters page.
- Credit-Based Calculation: Apply course credits when calculating GPA.
- Incomplete Courses: Exclude courses with missing assessment results from GPA calculations.
- Data Persistence: Store semester and course information in PostgreSQL.
- Input Validation: Validate user inputs and prevent duplicate course entries.

 Technologies Used

 Technology - Purpose 
 C# - Main programming language 
 .NET 10 - Application framework 
 Blazor Server - Web user interface 
 Entity Framework Core - Database access and ORM 
 PostgreSQL 17 - Relational database 
 xUnit - Unit testing 

 Architecture

GradePilot follows the Clean Architecture approach, separating the application into independent layers.

GradePilot

─ GradePilot.Domain
      Entities
      Grading
      Services

─ GradePilot.Application
      Interfaces
      Services

─ GradePilot.Infrastructure
      Persistence
      Repositories
      Migrations

─ GradePilot.Web
      Components
      Helpers
      Program.cs

─ GradePilot.Tests
       Unit Tests


Layer Responsibilities

- Domain: Contains core entities, grading rules, and GPA calculation logic.
- Application: Contains application services and repository interfaces, and coordinates business operations.
- Infrastructure: Implements database access, repositories, and Entity Framework Core configuration.
- Web: Provides the Blazor Server user interface and handles user interaction.
- Tests: Contains automated tests for verifying application logic.

 AITU Grading System

 Letter Grade - Grade Point - Percentage 
 A - 4.00 - 95–100 
 A- - 3.67 - 90–94 
 B+ - 3.33 - 85–89 
 B - 3.00 - 80–84 
 B- - 2.67 - 75–79 
 C+ - 2.33 - 70–74 
 C - 2.00 - 65–69 
 C- - 1.67 - 60–64 
 D+ - 1.33 - 55–59 
 D - 1.00 - 50–54 
 FX - 0.00 - 30–49 
 F - 0.00 - 0–29 

 Final Percentage Formula
Final Percentage = (Midterm × 0.30) + (Endterm × 0.30) + (Final Exam × 0.40)

 GPA Formula
GPA = Σ (Grade Point × Course Credits) / Σ Course Credits

Only completed courses are included in GPA calculations.

 Requirements

 .NET 10 SDK
 PostgreSQL 17
 Visual Studio Code or Visual Studio
 Entity Framework Core command-line tools:

dotnet tool install --global dotnet-ef


 Installation and Setup

 Step 1: Clone the Repository


git clone https://github.com/Rahmani-IT/GradePilot.git
cd GradePilot


 Step 2: Create the Database

Create a PostgreSQL database named gradepilotdb:

CREATE DATABASE gradepilotdb;


 Step 3: Configure the Connection String

Do not store real database passwords in committed files.
Store the connection string with .NET User Secrets from the GradePilot.Web project:


cd GradePilot.Web
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=gradepilotdb;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
cd ..


Replace YOUR_USERNAME and YOUR_PASSWORD with your local PostgreSQL credentials. The file appsettings.json contains only a placeholder value.

 Step 4: Apply Database Migrations

From the solution root directory, run:

dotnet ef database update --project .\GradePilot.Infrastructure\GradePilot.Infrastructure.csproj --startup-project .\GradePilot.Web\GradePilot.Web.csproj


 Step 5: Build the Project

dotnet build


 Step 6: Run the Application

dotnet run --project .\GradePilot.Web\GradePilot.Web.csproj

Open the local URL displayed in the terminal. The application is configured to run at:

http://localhost:5202

 Running Tests

To execute the automated tests, run:

dotnet test


The project currently has 35 passing tests.

 How to Use

1. Open GradePilot in your browser.
2. Create a new academic semester.
3. Add courses and enter their credit values.
4. Enter the midterm, endterm, and final exam scores. Leave them empty if they are not available yet.
5. View the calculated course percentages and AITU letter grades.
6. Check the semester GPA on the semester details page.
7. View your cumulative GPA (CGPA) at the top of the My Semesters page.

 Database

GradePilot uses PostgreSQL to persist semester and course information.
Entity Framework Core is used for database operations and migrations.

 Project Purpose

The main purpose of GradePilot is to provide AITU students with a simple and organized way to manage academic results and calculate GPA and CGPA according to the university's grading system.


Project: GradePilot
Framework: .NET 10
Architecture: Clean Architecture
Database: PostgreSQL
University Context: Astana IT University (AITU)
