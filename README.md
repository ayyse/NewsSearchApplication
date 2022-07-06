# News Search Worker Service Application

### Aim of Project
Using .Net Core Worker Service, writing a service that lists the current news by sending requests to news sites via API for desired keywords.

### What is Windows Service?
Windows services are software that do not have a user interface and run in the background in the operating system. Logging with Windows services, automatic mail sending, FTP file transfer, checking for program updates, etc. transactions can be performed.

### What is .Net Core Worker Service?
Worker Service is background services that run as cross-platform, just like windows services. Worker Service does not directly implement IHostedService. Inherits from BackgroundService. BackgroundService offers ExecuteAsync(), an abstract method that can run as a scheduler throughout the application, and the Dispose() method that allows us to clean up the Garbage Collector memory side.

- When the ExecuteAsync method runs the project, it refreshes every 10 seconds and runs the commands inside the method.
```
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
        }
}
```

- The output of the above method in the console is as follows, and the request continues every ten seconds until the project is closed.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim1.png">
</p>

### Entity Framework Code First

- It is a technique that establishes a link between a database and a programming language. It is an approach that allows you to perform your database operations in your project by writing code on the Visual Studio side as much as possible. Thanks to this approach, the relationship between the database interface and the software developer is minimized.

- In the Code First structure, the "class" structures in the programming language correspond to the "table" structures in the database, and the "property" structures correspond to the "column" structures in the database.

- In order to use the Code first structure, I right-clicked on the Core layer and loaded the following packages into the project from the Manage NuGet Packages section in the window that opened.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim2.png">
</p>
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim3.png">
</p>

### Creating Database with Entity Framework Code First

- I created two separate classes for news and keywords. I created the context class that will connect with the database and introduced the classes that will create the tables.
```
public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<News> News { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
    }
```

- I introduced the database address to connect to the appsetting.json file and added the address to connect in the program.cs class.
```
"ConnectionStrings": {
    "MsSQLConnection": "Server=DESKTOP-6L0RRSM\\SQLEXPRESS;Database=NewsSearchDB;User ID=NewsSearchDB;Password=NewsSearchDB;"
  }
```
```
services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(hostContext.Configuration.GetConnectionString("MsSQLConnection")));
```

- I selected the NewsSearch.Core layer, which is the data layer of my project, through the Package Manager Console and entered the `add-migration InitialCreate` command.

- After running the command, a file named Migrations was created and the codes to connect to the database were added automatically.

- Then I entered the `update-database` command in the Package Manager Console and the classes I created here were added to the database as tables.


### Using NewsApi to Pull News

- I ran the `Install-Package NewsAPI` command in the Package Manager Console and uploaded the api I will use to my project. Afterwards, I imported the necessary libraries and with the api key given to me, I pulled the current news in Turkey, in Turkish, and in the order of publication.

```
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
```

```
var newsApiClient = new NewsApiClient(Configuration["ApiKey"]);

var articleResponse = newsApiClient.GetEverything(new EverythingRequest
{
    Q = "Türkiye",
    Language = Languages.TR,
    SortBy = SortBys.PublishedAt,
    From = new DateTime(2022, 5, 17)
});
```

### Creating Windows Service using Background Service

- I installed the following packages to be able to run the project as a windows service.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim4.png">
</p>

- The UseWindowsService extension method is configuring the application to run as a Windows Service. I set "News Search Service" as the service name and then made the necessary adjustments.
```
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseWindowsService(options =>
        {
            options.ServiceName = "News Search Service";
        })        
```

### Publish the App

- I right-clicked on the NewsSearch.Worker project and opened the publish option in the window that opened. In the window that opens, I selected Folder and clicked the Next button. Then I made the necessary adjustments in the opened window.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim5.png">
</p>
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim6.png">
</p>

- I ran the command prompt as administrator and entered the following command.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim7.png">
</p>

```
sc.exe start "News Search Service"
```

<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim8.png">
</p>

- I controlled the data coming to the database and made the necessary updates in the codes. In the last case my project runs successfully and the data is correctly saved in the database.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim9.png">
</p>






