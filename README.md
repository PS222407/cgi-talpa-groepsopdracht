## Handige info
Voor de onderstaande commands moeten uitgevoerd worden in "cgi-talpa-groepsopdracht\source\Talpa\Talpa_10_WebApp"  

Deze command maakt een nieuwe migratie bestand aan  
```bash
dotnet ef migrations add CreateTable --project="../Talpa_30_DataAccessLayer"
```
Deze command voert de migraties uit 
```bash
dotnet ef database update --project="../Talpa_30_DataAccessLayer"
```
Zet de volgende user secrets onder dit voorbeeld command
```bash
dotnet user-secrets set "key" "value"
```
```
"Auth0:Domain"  
"Auth0:ClientId"  
"Auth0:ClientSecret"  
"Auth0:ApiClientId"  
"Auth0:ApiClientSecret"  
```
Create a team manually in your developemnt database with teamId 1, here all users are assigned to due to conflicts when changing team id in Auth0 database

## Tests
UITests must be runned from the <b>run_project_in_testing_env.bat</b> file in the source folder. But you first have to put an image named sample.jpg in C:\images folder.
  
You might want to debug and start all the services manually. To do this follow these steps:  

Go into Talpa_10_Web and run 
```bash
dotnet run
```

And start the UI test from IDE
