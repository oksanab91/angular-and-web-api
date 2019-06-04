# Angular.NET Core & Web ASP.NET Core API projects in One Solution


## For Client: 
  Create ASP.NET Core Web application with the <b>Angular project template</b> (Angular.Net).

## For Server: 
  Add new ASP.NET Core Web application with the <b>API project template</b> to the same solution (WebAPIs.AspNetCore.ExistingDb).
<br><br><br>

## Server-side

### Edit WebAPIs.AspNetCore.ExistingDb project

- Scaffold db context - database first approach:
  > Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=TraineeDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context "TraineesContext"

- Add new connection string to appsettings.json:
	> ConnectionStrings": {
		"ConnectionTrainees": "Server=(localdb)\\mssqllocaldb;Database=TraineeDb;Trusted_Connection=True;"}

- In Startup.cs, update ConfigureServices, add:
  > services.AddDbContext<TraineesContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("ConnectionTrainees")));

- In TraineesContext class, remove method:
  > OnConfiguring;
	
- To allow http requests from the angular client and to solve CORS issue, modify ConfigureServices in Startup.cs:
  > services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
  - and add [EnableCors("AllowOrigin")] attribute to the Controllers;

- Set as Startup project;

- Run the solution - APIs are available now.


## Client-side

### Modify Angular.Net Project in VS Code editor

- Open project from *ClientApp* folder and add all relevant classes and components;

- Create http requests to retrieve, update & display the data;

- In app.module.ts, add routerLinks for the components;

- Run `ng serve` for a dev server.

###
Useful info: [Use the Angular project template with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-2.2&tabs=visual-studio)
