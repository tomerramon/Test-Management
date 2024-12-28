# Test-Management
The project is a platform for Exams; users can upload their own Exams and take Exams.

## Students

- **Tomer Ramon** - 204621510

---

## Project URL

- [Localhost:7070](http://localhost:7070)

---

## Description

This project is a comprehensive platform designed for managing and participating in exams. It facilitates seamless interaction between teachers and students by providing specific functionalities tailored to each role.

### Features

#### Teachers:

- **Create Exams**: Design new exams with customizable formats and questions.
- **Upload Exams**: Upload pre-existing exams for students to access.
- **Edit Exams**: Modify exam details, such as questions, duration, or scoring.
- **Delete Exams**: Remove outdated or incorrect exams.
- **View Statistics**: Access detailed analytics on exam performance, participation, and other metrics.

#### Students:

- **Search for Exams**: Use filters and search options to find relevant exams.
- **Take Exams**: Enter and solve exams in an interactive environment with real-time feedback.

---

## Comments

- The code is well-documented, with comments provided for each function.
- Comments describe the purpose, functionality, and usage of the functions, ensuring clarity for developers and contributors.

---

## Running Instructions

Follow the steps below to set up and run the project on your local machine:

### Step 1: Navigate to the Client Folder

- Locate and open the `Telhai.CS.Final.Client.sln` file within the client folder.

### Step 2: Configure Startup Projects

1. Open Visual Studio (VS).
2. Right-click on the solution in the Solution Explorer panel.
3. Select **"Configure Startup Projects..."** from the context menu.
4. In the popup dialog:
   - Choose **"multiple startup projects"**.
   - For each project listed, set the action to **"Start"**.
   - Click **Apply** and then **OK**.

### Step 3: Install Dependencies

1. Open the **Package Manager Console** in Visual Studio.
2. Check the solution's `packages.config` or `*.csproj` files to identify required NuGet packages.
3. Install each package by running the command below, replacing `[PackageName]` and `[VersionNumber]` with the appropriate values:
   ```
   Install-Package [PackageName] -Version [VersionNumber]
   ```
4. If dependencies are missing, restore them collectively by executing:
   ```
   dotnet restore
   ```
5. Verify that all necessary packages are successfully installed and up to date.

### Step 4: Update the Database

- Use the Package Manager Console to apply database migrations and updates.
- Run the following command to update the database:
  ```
  Update-Database
  ```

### Step 5: Run the Project

1. Verify that all dependencies are installed, and the database is updated.
2. Click the **Start** button (green arrow) at the top of Visual Studio to launch the project.

---

## Additional Notes

- Ensure that your environment has the necessary software installed, including:

  - **Visual Studio** (latest version recommended).
  - **.NET Framework** or **.NET Core SDK** (based on the project requirements).
  - **SQL Server** or another database server for handling data storage.

- If the project fails to run, check the logs for errors or missing dependencies, and address them accordingly.

- Reach out to the project maintainers for further assistance if needed.

---

