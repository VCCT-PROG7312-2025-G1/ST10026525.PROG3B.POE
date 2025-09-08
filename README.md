# Municipal Services Web App

### A web application built using ASP.NET MVC for managing and accessing municipal services. This application allows residents to submit service requests, report issues, view announcements, and track the status of their requests.

## Table of Contents
1. Features
2. Prerequisites
3. Installation
4. Running the Application
5. Usage


### Features
- Submit service requests and report municipal issues.
- View local events and announcements.
- Track status of submitted requests.
- User-friendly and responsive interface.
- Admin dashboard for managing requests and announcements.

### Prerequisites
Before you start, ensure you have the following installed:
- **Visual Studio 2019/2022** (Community or higher) with **ASP.NET** 
- **.NET Framework 4.7.2+** (or your project’s target version).
- Internet browser (Chrome, Edge, Firefox).

### Installation
 Clone this repository:
1. git clone https://github.com/your-username/municipal-services-app.git
2. Open the solution file in Visual Studio (MunicipalServices.sln).
3. Restore NuGet packages:
   - Right-click the solution → Restore NuGet Packages.
4. Build the solution:
   - Press Ctrl + Shift + B or go to Build → Build Solution.

### Running the Application
1. Set the **Startup Project**:
 - Right-click on the project → **Set as Startup Project.**
2. Run the application:
 - Press F5 (Debug) or Ctrl + F5 (Run without debugging).
The default browser will open and navigate to https://localhost:5001 (or another port configured in your project).

### Usage
1. **Main Menu:** Navigate to key areas like “Report Issues”, “Service Requests”, and “Announcements”.
2. **Report Issues:** Fill out the form to submit issues to the municipality.
3. **Local Events and Announcements:** Coming Soon!
4. **Service Request Status:** Coming Soon!
*The application uses in-memory data structures (lists, queues, stacks) instead of a database to store data temporarily.*
