<img align="left" width="100" height="100" src="https://user-images.githubusercontent.com/70466418/102879400-59d3ec80-4452-11eb-8214-e92c9d2e21c0.png">

# trackIt


[![Build Status](https://dev.azure.com/rolfindc/trackId/_apis/build/status/trackId-ASP.NET-CI?branchName=main)](https://dev.azure.com/rolfindc/trackId/_build/latest?definitionId=2&branchName=main)


The purpose of this web application is to improve team's workflow by simply tracking bugs and assign them to members of your team.

## Template Structure

- **TrackIt.UI**: An ASP.NET MVC project.
  - Aggregates folder is the core of the application;
  - In Infrastructure folder can be found repositories.
 - **TrackIt.Test**: A Unit Test project which containes test for aggregates.

## Progress
- [x] User Interface (bootstrap 4 and JavaScript for some effects)
- [x] Registration (to accomplish this I used Identity - cookie-based authentication)
- [x] Use a DI container (I used AutoFac)
- [x] Roles creation & assign them to users
- [x] Implement project creation and UI
- [x] Change project workers
- [x] Create tickets for a project and assign them to workers
- [x] Change ticket worker
- [ ] Change ticket state
- [ ] Edit ticket (is implemented just a part of UI for this task)
- [ ] Edit project + UI (description)
- [ ] Delete project

## How to use

Use the following command to clone this repository.
```
git clone https://github.com/rolfintatu/trackIt.git
```

First of all in order to make the application to work you have to provide a connection string (in case you don't know what is a connection string read more about it [here](https://en.wikipedia.org/wiki/Connection_string)) for database creation. If you are asking: "How to do that?", the answear is simple, go to Web.config and replace the value for "connectionString" with your connection string and don't delete "providerName". 
Next step is to enable migrations, all you have to do is to use the following command in Package Manager Console, be sure you have "Default Project" set as "src\TrackIt.UI":
```
Enable-Migrations -ContextTypeName TrackIt.UI.Infrastructure.ApplicationDbContext
```
After running the above command, run next command to create "Init" migration:
```
Add-Migration "Init"
```
And the last command is:
```
Update-Database
```
This last step will create a database for you and apply all migration to it.


After creating the database you can start the application. If when it starts will show you a 404 error, naviate to "/Account/Register" and create you own account. If registration works without problems it will redirect you to "/Portal" page (at that moment page will display you a welcome message). From here you can navigate to "Projects" page:

![create_project](https://user-images.githubusercontent.com/70466418/103475556-b28e7800-4db6-11eb-8dec-d337c5e47389.PNG)

First time when you will navigate to "Projects", the page will be empty, so you have to create a project.

For creating a new project press on "Create" and it will redirect you to "Create" page:

![create_page_project](https://user-images.githubusercontent.com/70466418/103475598-22046780-4db7-11eb-84b6-4b5513d6a757.PNG)

In order to create the project you have to complete all the fields and assign at least one worker to it. If the project will be successfully created you'll be redirected to "Projects" page. Pressing on "Details" button from project card you'll be able to manage the project, create tickets for it and assign new workers to it.

# License
See [MIT License](https://github.com/rolfintatu/trackIt/blob/main/LICENSE).
