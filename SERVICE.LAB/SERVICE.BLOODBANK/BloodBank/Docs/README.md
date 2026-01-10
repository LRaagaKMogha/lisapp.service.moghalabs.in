dotnet new sln -o BloodBank
cd BloodBank
dotnet new classlib -o BloodBank/MasterManagement.Contracts
dotnet new webapi -o BloodBank/MasterManagement
dotnet sln add ./BloodBank/MasterManagement ./BloodBank/MasterManagement.Contracts
dotnet add .\BloodBank\MasterManagement\ reference .\BloodBank\MasterManagement.Contracts\

dotnet new classlib -o BloodBank/BloodBankManagement.Contracts
dotnet new webapi -o BloodBank/BloodBankManagement
dotnet sln add ./BloodBank/BloodBankManagement ./BloodBank/BloodBankManagement.Contracts
dotnet add .\BloodBank\BloodBankManagement\ reference .\BloodBank\BloodBankManagement.Contracts\
dotnet add .\BloodBank\BloodBankManagement\ reference .\BloodBank\MasterManagement\

dotnet new classlib -o BloodBank/Shared
dotnet sln add ./BloodBank/Shared 
dotnet add .\BloodBank\Shared\ reference .\BloodBank\BloodBankManagement.Contracts\
dotnet add .\BloodBank\Shared\ reference .\BloodBank\MasterManagement.Contracts\

dotnet add .\BloodBank\MasterManagement\ reference .\BloodBank\Shared\
dotnet add .\BloodBank\BloodBankManagement\ reference .\BloodBank\Shared\

dotnet build 
dotnet watch run --project ./BloodBank/MasterManagement/
dotnet watch run --project ./BloodBank/BloodBankManagement/
dotnet tool install --global dotnet-ef
dotnet add .\BloodBank\MasterManagement\  package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.11
dotnet add .\BloodBank\MasterManagement\  package Microsoft.EntityFrameworkCore.Design -v 6.0.11

dotnet ef migrations remove --project .\BloodBank\MasterManagement\
dotnet ef migrations add InitialCreate --project .\BloodBank\MasterManagement\
dotnet ef database update --project .\BloodBank\MasterManagement\

dotnet ef dbcontext script -o .\..\..\..\rmg-lis-db\BaseScript\MastersData\2.BB_mastermanagement.sql --project .\BloodBank\MasterManagement\ 

dotnet ef dbcontext script -o .\..\..\..\rmg-lis-db\BaseScript\MastersData\3.BB_bloodbankmanagement.sql --project .\BloodBank\BloodBankManagement\


dotnet add .\BloodBank\MasterManagement\ package Serilog.AspNetCore
dotnet add .\BloodBank\MasterManagement\ package FluentValidation.AspNetCore