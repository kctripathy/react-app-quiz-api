
https://www.syncfusion.com/blogs/post/how-to-build-crud-rest-apis-with-asp-net-core-3-1-and-entity-framework-core-create-jwt-tokens-and-secure-apis.aspx


This package helps generate controllers and views.
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 2.1.1

This package helps create database context and model classes from the database.
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.1

Database provider allows Entity Framework Core to work with SQL Server.
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.1.1



Scaffold-DbContext “Server=TSLC0750\SQLEXPRESS;Database=Quiz;Integrated Security=True;user id=sa;password=maa@1234;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Scaffold-DbContext “Server=TSLC0750\SQLEXPRESS;Database=Quiz;Integrated Security=True;user id=sa;password=maa@1234;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables Quiz_Countries




It provides support for creating and validating a JWT token.
Install-Package IdentityModel.Tokens.Jwt -Version 5.6.0

This is the middleware that enables an ASP.NET Core application to receive a bearer token in the request pipeline.
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 2.1.1



Repository Pattern:
====================
https://medium.com/net-core/repository-pattern-implementation-in-asp-net-core-21e01c6664d7
https://github.com/kilicars/AspNetCoreRepositoryPattern
