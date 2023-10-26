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