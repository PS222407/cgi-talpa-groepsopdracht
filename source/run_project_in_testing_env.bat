cd .\Talpa\Talpa_10_WebApp\

copy appsettings.Development.json appsettings.Development.backup.json
copy appsettings.Testing.json appsettings.Development.json

start "Talpa" dotnet run
cd ..

cd .\TalpaSeeder\
start "TalpaSeeder" php artisan serve
cd ..

timeout /nobreak /t 20

dotnet test --no-build --verbosity normal ./Talpa_UITests

copy appsettings.Development.backup.json appsettings.Development.json 

pause