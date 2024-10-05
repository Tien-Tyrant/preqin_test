# Preqin Test Project

This project contains both a backend (ASP.NET Core API) and a frontend (React) application that interact to list investors and filter them by asset class.

## Demo Website

The demo website can be accessed at the following URL:

**http://ec2-18-132-49-97.eu-west-2.compute.amazonaws.com/**

### Important Notes:
- The demo website currently supports **HTTP only**, not HTTPS. Please ensure you are accessing the site using `http://` and not `https://`.
- The frontend and backend are both hosted on the same EC2 instance.

## Features

- **Frontend**: A React TypeScript app for managing and displaying investor data.
- **Backend**: An ASP.NET Core API providing data to the frontend.

## Prerequisites

Before you begin, ensure you have the following tools installed:

- **Docker**
- **Docker Compose**

### Project Structure

```bash
/root
  ├── PreqinAPI           # Backend (ASP.NET Core API)
  ├── preqin-frontend      # Frontend (React)
  └── docker-compose.yml   # Docker Compose configuration
```

## Running the Project Locally

Follow these steps to run the project locally using Docker and Docker Compose:

1. **Clone the Repository**:

   Clone this repository to your local machine:

   ```bash
   git clone https://github.com/Tien-Tyrant/preqin_test.git
   cd preqin_test
   ```

2. **Build and Run the Containers**:

   In the root directory where the docker-compose.yml file is located, run the following command to build and start both the backend and frontend services:

   ```bash
   docker-compose up --build
   ```

   This command will:

   - Build the Docker images for both the backend (ASP.NET Core API) and frontend (React).
   - Start the backend on port 5000 and the frontend on port 80.

3. **Access the Applications**:

   Once the containers are up and running, open your browser and go to:

   - Frontend: http://localhost
   - Backend: http://localhost:5000/api/investor
