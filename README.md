# poc-state-machine-elsa

## objectives

- check the management of workflows (designer, etc...)
- check the versioning of workflows
- implement a custom activity
- check that a workflow can be triggered programmatically
- check state propagation between activities
- check versioning o factivities
- check execution logging
- check pause and restart of a workflow
- evaluate performance
- check ELSA roadmap

## setup

Before you can run the dotnet tool dotnet ef, you will need to install it.  
To install dotnet ef, run the following command from your terminal: 

```
dotnet tool install --global dotnet-ef 
```

Make sure to first run the SqlServer migrations against your SQL Server:

```
SET EF_CONNECTIONSTRING=Server=(localdb)\mssqllocaldb;Database=PocDB;Trusted_Connection=True;
dotnet ef database update --context SqlServerContext
```



## references

[ELSA official web site](https://elsa-workflows.github.io/elsa-core/)  
[Building Workflow Driven .NET Core Applications with Elsa](https://sipkeschoorstra.medium.com/building-workflow-driven-net-core-applications-with-elsa-139523aa4c50)

## dashboard

![](https://github.com/guerinsylvain/poc-state-machine-elsa/blob/main/doc/dashboard-001.jpg)  
![](https://github.com/guerinsylvain/poc-state-machine-elsa/blob/main/doc/dashboard-002.jpg)  
![](https://github.com/guerinsylvain/poc-state-machine-elsa/blob/main/doc/dashboard-003.jpg)
