
https://www.syncfusion.com/blogs/post/how-to-build-crud-rest-apis-with-asp-net-core-3-1-and-entity-framework-core-create-jwt-tokens-and-secure-apis.aspx


This package helps generate controllers and views.
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 2.1.1

This package helps create database context and model classes from the database.
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.1

Database provider allows Entity Framework Core to work with SQL Server.
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.1.1



Scaffold-DbContext “Server=TSLC0750\SQLEXPRESS;Database=Quiz;Integrated Security=True;user id=sa;password=maa@1234;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Scaffold-DbContext “Server=TSLC0750\SQLEXPRESS;Database=Quiz;Integrated Security=True;user id=sa;password=maa@1234;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables Quiz_Countries

Scaffold-DbContext "Server=TSLC0750\SQLEXPRESS_2014;Database=Quiz;Persist Security Info=False;User ID=sa;Password=elaw@1234;" Microsoft.EntityFrameworkCore.SqlServer  -Tables "Quiz_PasswordReset","Quiz_ContactLog" -context TempContext -ContextDir ".\Models" -OutputDir "Models" -force -DataAnnotations -UseDatabaseNames


I wish there were a built-in way to add entities and update an existing context, but there doesn't seem to be. I overcame this by using the --context option in the package manager console and just gave it a temporary name, e.g. --context TempContext. This worked and generated the new table and the temp context. Then I just copied the public virtual DbSet<NewEntityType> NewEntityType { get; set; } property and the modelBuilder.Entity<NewEntityType>(entity => block from the OnModelCreating method in the temp context to my existing one. After that, I deleted the temp context. It's pretty straightforward.

It provides support for creating and validating a JWT token.
Install-Package IdentityModel.Tokens.Jwt -Version 5.6.0

This is the middleware that enables an ASP.NET Core application to receive a bearer token in the request pipeline.
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 2.1.1



Repository Pattern:
====================
https://medium.com/net-core/repository-pattern-implementation-in-asp-net-core-21e01c6664d7
https://github.com/kilicars/AspNetCoreRepositoryPattern
