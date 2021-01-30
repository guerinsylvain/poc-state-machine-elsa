# poc-state-machine-elsa

Make sure to first run the SqlServer migrations against your SQL Server:

```
SET EF_CONNECTIONSTRING=Server=(localdb)\\mssqllocaldb;Database=PocDB;Trusted_Connection=True;
dotnet ef database update --context SqlServerContext
```
