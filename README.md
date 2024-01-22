
# TRACKSIFY - Employee Time Tracking System

TRACKSIFY is a modern Employee Time Tracking System designed to streamline project management, enhance transparency, and optimize resource utilization within organizations. This README provides essential information to get started with the application.

## Table of Contents

- [Features](#features)
- [Technological Stack](#technological-stack)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Contributing](#contributing)
- [Frontend Repository] (#frontend-repository)
- [License](#license)

## Features

- **User Authentication and Role Management:**
  - Secure sign-in process with role-based access control.
  
- **Project Dashboard:**
  - Personalized dashboard for users and admins displaying project details.

- **Project Management:**
  - Create and assign projects.

- **Real-Time Project Updates:**
  - Users can log work hours and submit updates.

## Technological Stack

- Backend:
  - .Net Web API
  - SQL Server Database
  - Entity Framework ORM

- Frontend:
  - Next.js
  - Tailwind CSS
  - TypeScript
  - React Query
  - Axios

- Additional Tools and Libraries:
  - AutoMapper
  - MimeKit
  - .NETCore.MailKit
  - Newtonsoft.Json
  - BCrypt.Net-Next
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore

## Getting Started

To run TRACKSIFY locally, follow these steps:

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Deolamma/Tracksify.git
   cd tracksify
   ```

2. **Install Dependencies:**
   ```bash
   # Backend
   cd backend
   dotnet restore

   # Frontend
   cd frontend
   npm install
   ```

3. **Run the Application:**
   ```bash
   # Backend
   cd backend
   dotnet run

   # Frontend
   cd frontend
   npm run dev
   ```

4. **Access the Application:**
   - Open your browser and go to `http://localhost:3000` to access the TRACKSIFY application.

## Database Setup

For database setup and data models, refer to the [Database Setup and Data Models](#database-setup-and-data-models) section in the documentation.

## Frontend Repository

The frontend for Tracksify is hosted on Netlify. For frontend details and deployment, refer to the Tracksify Frontend Repository on GitHub.

## Contributing

We welcome contributions! If you'd like to contribute to TRACKSIFY, please follow our [Contribution Guidelines](CONTRIBUTING.md).

## License

TRACKSIFY is licensed under the [MIT License](LICENSE).

---

