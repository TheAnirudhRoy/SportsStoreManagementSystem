# Sports Store Management System

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Project Overview New 

The Sports Store Management System is a comprehensive web application designed to manage the operations of a sports store efficiently. The project leverages the ASP.NET Core MVC framework with C# as the programming language, ensuring a robust and scalable architecture. The backend is powered by a Core Web API, and the entire application follows a layered architecture, separating concerns into the Presentation Layer, Business Layer, and Data Access Layer.

## Features

- **User Authentication and Authorization**: Implemented using Microsoft Identity, with two distinct roles - Admin and Clerk.
- **Inventory Management**: Admins can add suppliers, procure new products, and update store inventory.
- **Sales Management**: Clerks can interact with customers, sell products, generate bills, and initiate return processes.
- **Return Service**: Allows clerks to manage product returns, updating the inventory accordingly.
- **Supplier Management**: Admins can return purchased products to suppliers and update the inventory.

## Technologies Used

- **ASP.NET Core MVC**: For building the web application.
- **ASP.NET Core Web API**: For backend services.
- **C#**: Programming language.
- **Visual Studio Community 2022**: Integrated Development Environment (IDE).
- **Microsoft Identity**: For user authentication and authorization.
- **Layered Architecture**: Presentation Layer, Business Layer, Data Access Layer.

## Layered Architecture

1. **Presentation Layer**: 
   - Handles the UI/UX of the application.
   - Interacts with the users (Admins and Clerks).
   - Uses Razor Pages for rendering views.

2. **Business Layer**: 
   - Contains the business logic.
   - Processes data between the Presentation Layer and Data Access Layer.
   - Implements services for various functionalities like sales, inventory management, etc.

3. **Data Access Layer**: 
   - Manages database interactions.
   - Uses Entity Framework Core for ORM (Object-Relational Mapping).
   - Handles CRUD operations for entities like Products, Suppliers, Users, etc.

## User Roles

- **Admin**: 
  - Can add suppliers.
  - Procure new products and update inventory.
  - Return purchased products to suppliers.
- **Clerk**: 
  - Interacts with customers for selling products.
  - Generates bills.
  - Initiates and manages the return process of products.

## Usage

### Authentication and Authorization

- The application uses Microsoft Identity for managing user authentication and roles.
- Two roles are defined: `Admin` and `Clerk`.
- Role-based access control ensures that only authorized users can access specific features.

### Inventory Management

- **Admin**:
  - Add new suppliers.
  - Procure and add new products to the inventory.
  - Return products to suppliers if necessary.
- **Clerk**:
  - View inventory to assist customers.

### Sales and Return Process

- **Clerk**:
  - Interacts with customers to sell products.
  - Generates a bill for the purchased products.
  - Can initiate a return process if a customer wishes to return a product.
  - Updates the inventory upon successful return of products.

It involves managing and interacting with a Microsoft SQL Server database using SQL Server Management Studio (SSMS). This README provides instructions for installing SSMS, downloading a backup file, and setting up the project.

## Table of Contents

- [Installation](#installation)
  - [Installing Microsoft SQL Server Management Studio (SSMS)](#installing-microsoft-sql-server-management-studio-ssms)
  - [Downloading and Restoring the Database Backup](#downloading-and-restoring-the-database-backup)
  - [Setting Up the Project](#setting-up-the-project)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Installation

### Installing Microsoft SQL Server Management Studio (SSMS)

To manage and interact with the SQL Server database, you need to install SSMS. Follow these steps to install SSMS:

1. **Download SSMS:**
   - Go to the [official Microsoft download page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
   - Click on the download link for the latest version of SSMS.

2. **Run the Installer:**
   - Locate the downloaded installer file (usually named `SSMS-Setup-ENU.exe`) and double-click to run it.

3. **Install SSMS:**
   - In the installer window, click on the "Install" button.
   - Follow the on-screen instructions to complete the installation. The default settings are typically sufficient for most users.

4. **Launch SSMS:**
   - After the installation is complete, launch SSMS from the Start menu or search for "SQL Server Management Studio" in the Windows search bar.

### Downloading and Restoring the Database Backup

To set up the project database, you need to restore a backup file (.bak) provided with this project. Follow these steps:

1. **Download the .bak File:**
   - [Download the database backup file](https://github.com/TheAnirudhRoy/SportsStoreManagementSystem/blob/master/SportsDB.bak) and save it to a convenient location on your computer.

2. **Restore the Database in SSMS:**
   - Open SSMS.
   - Connect to your SQL Server instance.
   - In the Object Explorer, right-click on the `Databases` node and select `Restore Database...`.
   - In the `Source` section, select `Device` and click the `...` button to browse for the .bak file.
   - Click `Add` and locate the downloaded .bak file, then click `OK`.
   - In the `Destination` section, specify the name of the database you want to restore.
   - Click `OK` to start the restoration process. SSMS will restore the database from the .bak file.

### Setting Up the Project

1. **Clone the Repository:**
   ```sh
   git clone https://github.com/TheAnirudhRoy/SportsStoreManagementSystem.git
   cd SportsStoreManagementSystem
   
2. **Open in Visual Studio Community 2022:**
   - Open the solution file (.sln) in Visual Studio.
   
3. **Configure Database:**
   - Update the connection string in appsettings.json to point to your database.

4. **Open the Project in SSMS:**
   - Open SSMS.
   - Connect to your SQL Server instance.
   - Open the .sql files located in the project repository to set up additional database schema and populate initial data if needed.

5. **Configure the Database Connection:**
   - Modify the connection strings in your application configuration files (e.g., appsettings.json for .NET projects) to point to your SQL Server instance and the restored database.

6. **Run the Application:**
   - Press F5 or click on the Start button in Visual Studio to run the application.

Usage\
Provide instructions and examples on how to use your project. Include code snippets, screenshots, and other details to help users understand how to interact with your project.

License\
This project is licensed under the MIT License - see the LICENSE file for details.
