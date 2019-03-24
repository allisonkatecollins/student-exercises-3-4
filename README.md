# PART 3

# Student Exercises Database

In this part of building your application, you will be creating the database tables and the data that you will be querying in your application logic later.

You will use `CREATE TABLE` statements and `INSERT` statements to create all the tables necessary for storing information about student exercises in a SQL Server database.

## Setup

### Create the Database Script File

```sh
cd ~/workspace/csharp/StudentExercises
touch StudentExercises.sql
```

## Open the Database Script for Editing

1. Open SSMS.
1. Using the "File" menu, open the `StudentExercises.sql` file.

## Create a new Database

1. In the Object Explorer, right-click the Databases node.
1. Click "New Database" in the pop-up menu.
1. Enter a name for the new database.
1. Click "Ok".


## Creating Data

1. Ensure that your database is selected in the database dropdown located above the Object Explorer.
1. In the Query Window, enter the SQL to create all of your tables, columns, and foreign key constraints.

Then use `INSERT` statements to create data in your tables.


## Instructions

1. Create tables from each entity in the Student Exercises ERD.
1. Populate each table with data. You should have 2-3 cohorts, 5-10 students, 4-8 instructors,  2-5 exercises and each student should be assigned 1-2 exercises.

# PART 4

# Exploring Student Exercises in the database using <span>ADO</span>.NET

## Instructions

1. Create a new "**Console App (.NET Core)**" project.
1. Add the `System.Data.SqlClient` nuget package to your project.
1. Create a `Repository` class to interact with the `StudentExercises` database you created in Student Exercises Part 3.
1. Write the necessary C# code in `Repository.cs` and `Program.cs` to perform the following actions. Make sure to print results of each action to the console.
    1. Query the database for all the Exercises.
    1. Find all the exercises in the database where the language is JavaScript.
    1. Insert a new exercise into the database.
    1. Find all instructors in the database. Include each instructor's cohort.
    1. Insert a new instructor into the database. Assign the instructor to an existing cohort.
    1. Assign an existing exercise to an existing student.
