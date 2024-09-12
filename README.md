If you want to use SQL, follow these steps:

1. Create a user in your SQL database and assign the proper permissions.

2. In the appsettings file, under the “SqlSetting” section, enter the username and password for the user created in the previous step. Also, set IsEnable to true.

3. In Visual Studio, run the following command in the Package Manager Console: “Update-Database”.

---

Alternatively, if you prefer to use the in-memory database:

1. Set IsEnable to false in the “SqlSetting” section of the appsettings file.

2. Run the project, and in Swagger, execute all APIs that have "InMemory" in their names.

---

Note: A MongoDB repository has been created for future use (e.g., for saving user invoices, etc.).
