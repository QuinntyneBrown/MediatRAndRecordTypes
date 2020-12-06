dotnet ef database update 0
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet run ci
