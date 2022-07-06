# News Search Worker Service Application

### Aim of Project
Using .Net Core Worker Service, writing a service that lists the current news by sending requests to news sites via API for desired keywords.

### What is Windows Service?
Windows services are software that do not have a user interface and run in the background in the operating system. Logging with Windows services, automatic mail sending, FTP file transfer, checking for program updates, etc. transactions can be performed.

### What is .Net Core Worker Service?
Worker Service is background services that run as cross-platform, just like windows services. Worker Service does not directly implement IHostedService. Inherits from BackgroundService. BackgroundService offers ExecuteAsync(), an abstract method that can run as a scheduler throughout the application, and the Dispose() method that allows us to clean up the Garbage Collector memory side.

- When the ExecuteAsync method runs the project, it refreshes every 10 seconds and runs the commands inside the method.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim1.png">
</p>

- The output of the above method in the console is as follows, and the request continues every ten seconds until the project is closed.
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim2.png">
</p>

### Entity Framework Code First

- It is a technique that establishes a link between a database and a programming language. It is an approach that allows you to perform your database operations in your project by writing code on the Visual Studio side as much as possible. Thanks to this approach, the relationship between the database interface and the software developer is minimized.

- In the Code First structure, the "class" structures in the programming language correspond to the "table" structures in the database, and the "property" structures correspond to the "column" structures in the database.

- In order to use the Code first structure, I right-clicked on the Core layer and loaded the following packages into the project from the Manage NuGet Packages section in the window that opened.

<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim3.png">
</p>
<p align="center">
  <img src="https://github.com/ayyse/NewsSearchApplication/blob/main/Screenshots/Resim4.png">
</p>
