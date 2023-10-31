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
