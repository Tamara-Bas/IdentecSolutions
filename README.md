Setup and Instalation
-.Net sdk installed
-Sql server ans SSMS

Instalation steps
1.Clone the repository
2.Restore the dependencies
  -dotnet restore
3.Apply migrations
  -in appsetting.json file first add your database (located in IdentecSolutions.WebApi)
  -the in Package manage console you should set default project IdentencSolutions.EF and then execute this command -  update-database. 
Make sure that IdentecSolutions.WebApi is set as startup project

Api documentation
https://localhost:5000/swagger/index.html

Project structure
1.Application -Bussines logic, CQRS handlers
2.Domain - Enties
3.EntityFramework - infrastrucure, database
4.API - controllers and api setup

Tecnologies used
1. .NET 8
2.Entity framework
3.CQRS pattern
4.Fluent Validation
5.Swagger
5.Sql Server

Notes:
On master branch- there is init of architecture of the projcet
On main branch - there are all implementations
On exceptionMidellware branch- this branch is not merged into main because I didnt manage to make it fully functional.


