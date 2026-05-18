# Brädhörnan laboration

Ett C#-baserat projekt för att hantera medlemmar, spel och spelträffar för en förening.

## Funktioner
- Registrera nya medlemmar
- Uppdatera befintliga medlemmar
- Ta bort befintliga medlemmar
- Skapa spelträffar
- Reserver spel för spelträff
- LINQ funktioner för att filtrera medlemar efter status och sortera medlemmar i alfabetisk ordning samt gruppering av spel efter genre

  ## Tekniker
  - C#
  - .NET
  - WPF
  - Objektorienterad programmering (OOP)
  - LINQ
  - Enum-typer
  - WPF

  ## Domänmodell

  Projektet bygger främst på följande klasser:
  - Member.cs
  - Game.cs
  - GameMeeting.cs

  - MemberManager.cs
  - GameManager.cs
  - GameMeetingManager.cs

 ### Relationer
 - Ett 'GameMeeting' kan ha flera 'Member' som deltagare.
 - Ett 'GameMeeting' kan ha flera 'Game' som planerade spel.
 - Manager-klasserna ansvarar för skapande, sökning filtrering och borttagning.

   ## Installation

1. Klona projektet.
2. Öppna lösningen i Visual Studio.
3. Bygg projektet.
4. Kör applikationen.

## Användning

Exempel på hur ett spel kan skapas:

CSHARP-
var game = gameManager.AddGame(
    "Texas Hold'em",
    2,
    9,
    75,
    DifficultyLevelEnum.Medium,
    GamegenreEnum.Strategy);


Exempel på hur en medlemmar registreras:

CSHARP-
var member = memberManager.RegisterNewMember(
    "Franco",
    "Ortega",
    "fOrtega@hotmail.com",
    "073073073",
    MemberStatusEnum.Active,
    MemberRoleEnum.Member);


## Projektstruktur

- Models/ - Domänklasserna.
- Services/ - Manager-klasserna.
- Enum/ -  för roller, status, genre, spelsvårighet, eventtyper.
    
