# Planner

![Login](login.png)

## Introduction

Hassle-free hike planner is a unified collaboration platform that helps organizers easily plan logistics and coordinate with the group.   It does online research for you, and automatically generates trip plans based on group members' profile including their information and contribution to the group, to optimize driving, gear sharing and itinerary. Everyone gets their personalized to-do list for the trips, and the group is ready to have a safe and fun adventure together.

## Local development

Open appsettings.json file, and provide the value for `DB_CONNECTION_STRING` setting. You can find the connection string for the "Tahoma" SQL Database in the Azure portal, and replace the `{your_password}` placeholder with the password shared separately. Populate other settings as shown below.

Make sure to undo the changes to this file before checking in code changes.

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "AllowedHosts": "*",
    "DB_CONNECTION_STRING": "",
    "BITLY_BEARER_TOKEN": "",
    "SHOULD_QUERY_BITLY": false,
    "SHOULD_QUERY_WEATHER": true
}

```

## Folder structure

Controllers folder defines the operations (e.g.: CRUD - Create, Read, Update, Delete) for the various entities (e.g.: hiker, trip).

Models folder has the data structure for the SQL database table (e.g.: `Hiker`), and their corresponding model for the view (e.g.: `HikerViewModel`).

Scripts folder has the SQL scripts for creating the database tables, and populating them with some sample values. They have been executed against the SQL database, and can be re-run manually to roll back to the initial state.

Views folder has the web pages. 

`PlannerDbContext` has the connection to the SQL database.

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

## SQL queries

```
-- Add a new required column with default value
ALTER TABLE dbo.hiker
ADD user_name VARCHAR (50) NOT NULL DEFAULT '0';

-- Update specific column(s) of a row
UPDATE dbo.hiker
SET password = 'password', user_name= 'jimmy'
WHERE id = 2;
```