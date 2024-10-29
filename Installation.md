# Installation

### 1. Clone the repository

Begin by cloning the repository:

```console
    git clone https://github.com/DorijanJ/TeachABit.git
    cd TeachABit/src
```

### 2. Set up the backend environment

1. Navigate to TeachABit.API directory.
2. Create a "appsettings.development.json" file.
3. Copy contents of "appsettings.development.json.txt" into the newly created file.
4. Insert a JWT Key in the configuration file.
5. Insert other secrets if you have them.

### 3. Database setup

1. Create a new postgres user named "teachabit_backend" with a password.
2. Create a new PostgreSQL database named teachabit with a schema named backend.
3. Update database connection string in the "appsettings.development.json" file.
4. If not already installed, execute the following command:

```console
    dotnet tool install --global dotnet-ef --version 8.*
```
5. To set up the database schema, run the following command from the TeachABit/src directory:

```console
    dotnet ef database update -s TeachABit.API -p TeachABit.Model
```
### 4. Run the backend
```console
    dotnet restore
    dotnet run
```
### 5. Set up and run the frontend
1. Navigate to TeachABit.Front directory.
2. Create a ".env.development" file.
3. Copy contents of "appsettings.development.json.txt" into the newly created file.
4. Run the following commands from the TeachABit.Front directory:
```console
    npm install
    npm run dev
```
### 6. Usage
After setting up the project, open your web browser and navigate to https://localhost:3000 to access the TeachABit application.