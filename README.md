# Brädhörnan laboration
Detta är ett C#-baserat projekt för att hantera medlemmar, spel och spelträffar för en förening.
Detta projekt gäller för Laboration 1 och 2 i kursen objektorienterad programmering.

## Funktioner
Systemet stödjer följande funktioner.
- Registrera nya medlemmar
- Uppdatera befintliga medlemmar
- Ta bort befintliga medlemmar
- Skapa spelträffar
- Reservera spel för spelträff
- Lägga till nya spel
- Ta bort spel
- Uppdatera spel
- Använda LINQ-funktioner för att filtrera medlemmar efter status, sortera i alfabetisk ordning samt gruppera spel efter genre

## Tekniker
- C# / .NET 8
- WPF
- MVVM med CommunityToolkit.Mvvm
- Entity Framework Core
- LINQ

## Krav
- Visual Studio 2022
- .NET 8
- SQL Server LocalDB

## Domänmodell
Projektet bygger på följande domänklasser med affärslogik och validering:
- `Member.cs` — representerar en föreningsmedlem med namn, email, telefon, roll och status
- `Game.cs` — representerar ett spel med namn, genre, svårighetsgrad och tillgänglighet
- `GameMeeting.cs` — representerar en spelträff med datum, plats, eventtyp och max deltagare

Service-klasser som hanterar dataåtkomst:
- `MemberService.cs` — hämtar och sparar medlemmar i databasen
- `GameService.cs` — hämtar och sparar spel i databasen
- `GameMeetingService.cs` — hämtar och sparar spelträffar i databasen

## Relationer
- Ett `GameMeeting` kan ha flera `Member` som deltagare
- Ett `GameMeeting` kan ha flera `Game` som planerade spel
- En `GameMeeting` kan ha en ansvarig `Member`
- Service-klasserna (`MemberService`, `GameService`, `GameMeetingService`) 
  ansvarar för dataåtkomst mot databasen via Entity Framework Core

## Installation och start
1. Klona projektet
2. Öppna lösningen i Visual Studio 2022
3. Kontrollera att SQL Server LocalDB är installerat
4. Kör migrationer i Package Manager Console: Update-Database
5. Starta applikationen med F5

Vid första start laddas demonstrationsdata automatiskt in i databasen med
10 medlemmar, 8 spel och 5 spelträffar.

## Projektstruktur
- `Models/` — Domänklasser (`Game`, `Member`, `GameMeeting`) med affärslogik och validering
- `Services/` — Service-klasser (`GameService`, `MemberService`, `GameMeetingService`) bakom interfaces som hanterar dataåtkomst mot databasen
- `ViewModels/` — ViewModel-klasser enligt MVVM-mönstret
- `Data/` — AppDbContext och demodata
- `Enum/` — Roller, status, genre, spelsvårighet och eventtyper
- `Migrations/` — Databasmigrationer
