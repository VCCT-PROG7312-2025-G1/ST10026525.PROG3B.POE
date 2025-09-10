# Municipal Services Web Application

## Overview
This web application allows citizens to interact with municipal services online. It is built using **ASP.NET MVC** and provides a simple, modern interface for reporting municipal issues, checking service request statuses, and viewing local events and announcements.

The main goal of this application is to provide a **user-friendly platform for municipal engagement**, allowing citizens to submit reports and track their requests digitally.

---

## Features

- **Report Issues**
  - Submit reports about municipal problems or service requests.
  - Optionally upload media files (images) to support the report.
  - Visual progress bar that updates as form fields are completed.
  
- **Service Request Status** (Coming Soon)
  - View the progress and status of previously submitted requests.

- **Local Events and Announcements** (Coming Soon)
  - Stay informed about local community events and municipal announcements.

---

## Technology Stack

- **Backend:** ASP.NET MVC (C#)
- **Frontend:** Razor Pages, Bootstrap 5, custom CSS
- **Database:** [Your database here, e.g., SQL Server / LocalDB]
- **Media Handling:** File uploads saved to `wwwroot/uploads`
- **Version Control:** GitHub

---

## UI / Design

- Inspired by a local South African municpality - City of Tswhane. We've used their color scheme as well as their logo's for this project. 
- Modern, responsive interface using **cards, progress bars, and styled forms**.
- **Green and gold theme** consistent across the application.
- Interactive menu page with icons and descriptions for each service option.

---

## How It Works

1. On startup, users are presented with a **Main Menu page** with three options:  
   - Report Issues  
   - Local Events & Announcements (Coming Soon)  
   - Service Request Status (Coming Soon)
2. Users can submit reports via a form that includes **location, category, description, and media upload**.
3. Reports are stored in the backend, and uploaded media is saved to the `wwwroot/uploads` folder.
4. Progress bars and notifications give the user visual feedback during form submission.

---

## Screenshots


<img width="1452" height="756" alt="Screenshot 2025-09-10 at 03 34 06" src="https://github.com/user-attachments/assets/6c8c5901-d286-4a2f-8b0a-ce00eedb19b8" />

<img width="1455" height="786" alt="Screenshot 2025-09-10 at 03 34 22" src="https://github.com/user-attachments/assets/90229afe-92f1-4b21-a315-00d8325bbbbd" />

---<img width="1461" height="295" alt="Screenshot 2025-09-10 at 03 34 33" src="https://github.com/user-attachments/assets/5f7f7f12-3d9d-4cb1-9f3e-55d015a1f39a" />

<img width="1450" height="763" alt="Screenshot 2025-09-10 at 03 34 42" src="https://github.com/user-attachments/assets/3106937a-5cec-46c0-bd04-954ae108821f" />

## Future Enhancements

- Fully implement the **Service Request Status** feature.
- Add **Local Events and Announcements** section.
- Enhance reporting with **email notifications** or dashboard analytics.

