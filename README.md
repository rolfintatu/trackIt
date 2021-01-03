<img align="left" width="100" height="100" src="https://user-images.githubusercontent.com/70466418/102879400-59d3ec80-4452-11eb-8214-e92c9d2e21c0.png">

# trackIt


[![Build Status](https://dev.azure.com/rolfindc/trackId/_apis/build/status/trackId-ASP.NET-CI?branchName=main)](https://dev.azure.com/rolfindc/trackId/_build/latest?definitionId=2&branchName=main)


The purpose of this web application is to improve team's workflow by simply tracking bugs and assign them to members of your team.

# Template Structure

- **TrackIt.UI**: An ASP.NET MVC project.
  - Aggregates folder is the core of the application;
  - In Infrastructure folder can be found repositories.
 - **TrackIt.Test**: A Unit Test project which containes test for aggregates.


# How to use

First of all in order to make the application to work you have to create the database (I'm using MSSql). For creating the database you have to provide a connection string. If you are asking: "How to do that?", the answear is simple, go to Web.config and replace "YOUR CONNECTION STRING" with your connection string and don't forggot to mention "providerName". For migrating the database I used [First Code Migrations](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/). By the way don't use ProjectContext for migrating your database, use ApplicationDbContext.

After create the database you can start the application. If when it start will show you a 404 error, naviate to /Account/Register and create you own account. If registration works without problems it will redirect you to /Portal page (at the moment page will display you a welcome message). From here you can navigate to "Projects" page:

![create_project](https://user-images.githubusercontent.com/70466418/103475556-b28e7800-4db6-11eb-8dec-d337c5e47389.PNG)

First time when you will navigate to "Project" page this will be empty, so you have to create a project.

For creating a new project press on "Create New" and will redirect you to "Create" page:

![create_page_project](https://user-images.githubusercontent.com/70466418/103475598-22046780-4db7-11eb-84b6-4b5513d6a757.PNG)

In order to create the project you have to complete all the filds and assign at least one worker to it. If the project will be successfully created you'll be redirected to "Projects" page. Pressing on "Details" button from project card you'll be able to manage the project, create tickets for it, assign them to workers from project.
