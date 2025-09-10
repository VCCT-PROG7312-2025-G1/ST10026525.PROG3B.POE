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

*(Optional: add screenshots of the form, menu page, and reports page here)*

---

## Future Enhancements

- Fully implement the **Service Request Status** feature.
- Add **Local Events and Announcements** section.
- Enhance reporting with **email notifications** or dashboard analytics.

