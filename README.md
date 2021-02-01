# poc-state-machine-elsa

## objectives

- check the management of workflows (designer, etc...)
- check the versioning of workflows
- implement a custom activity
- check that a workflow can be triggered programmatically
- check state propagation between activities
- check versionning o factivities
- check execution logging
- check pause and restart of a workflow
- evaluate performance

## setup

Make sure to first run the SqlServer migrations against your SQL Server:

```
SET EF_CONNECTIONSTRING=Server=(localdb)\\mssqllocaldb;Database=PocDB;Trusted_Connection=True;
dotnet ef database update --context SqlServerContext
```

## references

[ELSA official web site](https://elsa-workflows.github.io/elsa-core/)  
[Building Workflow Driven .NET Core Applications with Elsa](https://sipkeschoorstra.medium.com/building-workflow-driven-net-core-applications-with-elsa-139523aa4c50)
