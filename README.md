# Mediatr-FluentValidation-StructureMap-ASP.NET-Core-Boilerplate
*Requires ASP.NET CORE RC2* This boilerplate is helping someone who is trying to start up the CQRS project.
It provide the bearer token authorization method, Logging in the file and database, validation in Mediatr handler.

#Used Plugin
1. OpenIDdict
2. Entityframework Core
3. Automapper
4. Mediatr
5. StructureMap (For decorating the validation handler)
6. Nlog (For logging)

To run the boilerplate template app:

		1. Download it and restore the package & Change the appsettings.json and nlog.config to your DB & Run the SystemLog.sql
		2. Run Add-Migration MyFirstMigration on the package manager console
		3. Run Update-Database
		4. Build the Project by pressing F5

#References
1. http://stevejgordon.co.uk/cqrs-using-mediatr-asp-net-core <br />
2. https://lostechies.com/jimmybogard/2014/09/09/tackling-cross-cutting-concerns-with-a-mediator-pipeline/ <br />
3. https://lostechies.com/jimmybogard/2015/05/05/cqrs-with-mediatr-and-automapper/ <br />
4. https://github.com/openiddict/openiddict-core (As the OpenIDDict is updated and it support custom token endpoint right now, you should take a look on it)
