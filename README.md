# Brädhörnan laboration
Ett C#-baserat projekt för att hantera medlemmar, spel och spelträffar för en förening.
(Laboration 1, 2 & 3) för kursen objektorienterad programmering

## Funktioner
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
- C#
- .NET
- WPF
- Objektorienterad programmering (OOP)
- LINQ
- Enum-typer

## Domänmodell
Projektet bygger främst på följande klasser:
- `Member.cs`
- `Game.cs`
- `GameMeeting.cs`
- `MemberManager.cs`
- `GameManager.cs`
- `GameMeetingManager.cs`

### Relationer
- Ett `GameMeeting` kan ha flera `Member` som deltagare.
- Ett `GameMeeting` kan ha flera `Game` som planerade spel.
- Manager-klasserna ansvarar för skapande, uppdatering, sökning, filtrering och borttagning.

## Installation
1. Klona projektet.
2. Öppna lösningen i Visual Studio.
3. Bygg projektet.
4. Kör applikationen.

## Användning
Exempel på hur ett spel läggs till:
```csharp
var game = gameManager.AddGame(
    "Texas Hold'em",
    2,
    9,
    75,
    DifficultyLevelEnum.Medium,
    GameGenreEnum.Strategy);
```

Exempel på hur ett spel uppdateras:
```csharp
gameManager.UpdateGame(game, "Texas Hold'em", 2, 9, 75, DifficultyLevelEnum.Hard, GameGenreEnum.Strategy);
```

Exempel på hur en medlem registreras:
```csharp
var member = memberManager.RegisterNewMember(
    "Franco",
    "Ortega",
    "fOrtega@hotmail.com",
    "073073073",
    MemberStatusEnum.Active,
    MemberRoleEnum.Member);
```

## Projektstruktur
- `Models/` – Domänklasserna.
- `Services/` – Manager-klasserna.
- `ViewModels/` – ViewModel-klasser för WPF (MVVM-mönster).
- `Enum/` – Roller, status, genre, spelsvårighet och eventtyper.
- `Data/` – Databaskontext (AppDbContext).
- `Migrations/` – Databasmigrationer.
