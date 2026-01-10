dotnet new sln -o QC
cd QC
dotnet new classlib -o QC/QCManagement.Contracts
dotnet new webapi -o QC/QCManagement
dotnet sln add ./QC/QCManagement ./QC/QCManagement.Contracts
dotnet add .\QC\QCManagement\ reference .\QC\QCManagement.Contracts\

dotnet sln add ./../../Shared/

dotnet add .\QCManagement\ reference .\..\..\Shared\


dotnet build 
dotnet watch run --project ./QC/QCManagement/
dotnet watch run --project ./QC/QCManagement/
dotnet tool install --global dotnet-ef
dotnet add .\QC\QCManagement\  package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.11
dotnet add .\QC\QCManagement\  package Microsoft.EntityFrameworkCore.Design -v 6.0.11

dotnet ef migrations remove --project .\QCManagement\
dotnet ef migrations add InitialCreate --project .\QCManagement\
dotnet ef database update --project .\QCManagement\

dotnet ef dbcontext script -o .\Scripts\QC.sql --project .\QCManagement\

dotnet add .\QC\QCManagement\ package Serilog.AspNetCore