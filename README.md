# Planner

## Database access

Azure subscription: \<to be shared\>
Resource group: Hackathon2021
SQL server name: `tahoma`
SQL database name: `tahoma`

To view and edit the database locally, you can use [SQL Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15) with the following configuration:

- Server type: Database Engine
- Server name: `tcp:tahoma.database.windows.net,1433`
- Authentication: SQL Server Authentication
- Login: `tahoma`
- Password: \<to be shared\>

When prompted, sign it to your Azure account.

If you get an error message saying your IP addressed is not allowed, go to the SQL Database in the Azure portal. On the overview page, click "Set server firewall", and add your IP there. Give it any rule name, and you can use your same IP for Start IP and End IP.

![SQL Server login](tahomadb.png)

## Local development

Open appsettings.json file, and provide the value for `DB_CONNECTION_STRING` setting. The connection string for the "Tahoma" SQL Database can be found in the Azure portal. 

## Folder structure

```
Planner/
├─ Controllers/
│  ├─ HikerController.cs
│  ├─ HomeController.cs
│  ├─ ResourcesController.cs
│  ├─ ...
├─ Models/
│  ├─ Hiker.cs
│  ├─ HikerViewModel.cs
│  ├─ ...
├─ Scripts/
│  ├─ create_tables.sql
│  ├─ insert_hiker.sql
│  ├─ ...
├─ Views/
│  ├─ Hiker/
│     ├─ Create.cshtml
│     ├─ Delete.cshtml
│     ├─ Details.cshtml
│     ├─ Edit.cshtml
│     ├─ Index.cshtml
│  ├─ Home/
│     ├─ ...
│  ├─ Resources/
│     ├─ ...
├─ PlannerDbContext
├─ README.md
├─ ...
```