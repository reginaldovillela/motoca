## Commands to genarate migrations
dotnet ef migrations add bikes -p .\src\Motoca.Infrastructure\ -s .\src\Motoca.API -c BikesContext -o Migrations\Bikes
dotnet ef migrations add rentals -p .\src\Motoca.Infrastructure\ -s .\src\Motoca.API -c RentalsContext -o Migrations\Rentals
dotnet ef migrations add riders -p .\src\Motoca.Infrastructure\ -s .\src\Motoca.API -c RidersContext -o Migrations\Riders