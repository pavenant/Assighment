Setup Instructions

1.	Create new SQL Server 2016 database. Name of databased used in the supplied connection string is "Assignment", but other database name can also be used. 
2.	Run DBSetup.sql to setup fresh Database.  If only an earlier version is available, the create and insert scripts will still work be drop statements may not.
3.	Setup connection string for website to work. DB connection string: appsettings.json
4.	To run Integration Tests, change connection string in: Pierre.Avenant.Test\IntegrationTests\Infrastructure\Database\Configuration.cs (ran out of time to figure out how .net core works with connection strings in test projects.)

General Notes and things still todo:
1.	Used .net core with Visual Studio. 
2.	The SQL Bulk copy is not .net core compliant, not found time to investigate a compliant alternative.
2.	Still have to implement paging for big files.
3.	Still have to implement filter /edit/add/update for Account Transactions
