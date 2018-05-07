Setup Instructions

1.	Create new SQL Server 2016 or above database.
2.	Run DBSetup.sql into DB
3.	Setup connection string for website to work. DB connection string: appsettings.json
4.	To run Integration Tests, change connection string in: Pierre.Avenant.Test\IntegrationTests\Infrastructure\Database\Configuration.cs

General Notes and things still todo:
1.	The SQL Bulk copy is not .net core copliant yet.
2.	Still have to implement paging for big files.
3.	Still have to implement filter /edit/add/update for Account Transactions
