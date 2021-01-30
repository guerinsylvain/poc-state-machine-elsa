# poc-state-machine-elsa

## objectives

- validate the management possibilites of workflow (dashboard designer)
- check the versioning of workflows
- implement a custom activity

## setup

Make sure to first run the SqlServer migrations against your SQL Server:

```
SET EF_CONNECTIONSTRING=Server=(localdb)\\mssqllocaldb;Database=PocDB;Trusted_Connection=True;
dotnet ef database update --context SqlServerContext
```
