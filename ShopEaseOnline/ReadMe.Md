# Project Setup Guide

## Getting Started

Follow these steps to set up the project environment:

1. **When downloaded, delete any existing migrations**
   - Navigate to the Migrations folder and remove all files

2. **Set up the database**
   - Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
   - Run the following commands:
     ```
     Add-Migration InitialCreate
     Update-Database
     ```

3. **Deploy initial data**
   - Run the `DataDeploymentScript.sql` located in the Scripts folder

4. **You're all set!**
   - The application is now ready to run

