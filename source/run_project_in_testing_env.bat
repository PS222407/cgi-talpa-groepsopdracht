cd .\Talpa\Talpa_10_WebApp\

copy appsettings.Development.json appsettings.Development.backup.json
copy appsettings.Testing.json appsettings.Development.json

start "Talpa" dotnet run
cd ..

cd .\TalpaSeeder\
REM start "TalpaSeeder" php artisan serve
cd ..

timeout /nobreak /t 15

dotnet test --no-build --verbosity normal ./Talpa_UITests

cd .\Talpa_10_WebApp\

copy appsettings.Development.backup.json appsettings.Development.json 

pause