# Daily Expenses Sharing Application

## Overview

This is a backend service for a daily expenses sharing application. The application allows users to add expenses and split them based on three 
different methods: exact amounts, percentages, and equal splits. It also provides functionalities to manage user details, validate inputs, 
and generate downloadable balance sheets.

## Features

- **User Management:**
  - Create user
  - Retrieve user details

- **Expense Management:**
  - Add expense
  - Retrieve individual user expenses
  - Retrieve overall expenses
  - Download balance sheet

- **Expense Split Methods:**
  - Equal: Split equally among all participants
  - Exact: Specify the exact amount each participant owes
  - Percentage: Specify the percentage each participant owes (Ensure percentages add up to 100%)

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use the in-memory database for development/testing)
- [Visual Studio Code](https://code.visualstudio.com/) (or any preferred IDE)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## run the project 
- clone the project 
- open the project using visual studio
- install nessacary nuget package
- setup the database
- run the migration commond
- now run the project
