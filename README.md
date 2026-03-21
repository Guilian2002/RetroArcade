🕹️ Retro’s Arcade Room

    La plateforme qui digitalise l'arcade.
    L'alliance de la performance brute ADO.NET et d'une esthétique Cyberpunk immersive.

Retro’s Arcade Room n'est pas qu'un simple outil de gestion. C'est un écosystème complet qui permet de piloter la rentabilité des succursales en temps réel tout en honorant l'héritage du rétrogaming.

🏛️ Architecture de la Solution
Le projet est segmenté en 6 modules spécialisés pour garantir une séparation stricte des responsabilités :

    RetroArcade.Blazor : Le Front-end Framework Blazor et ses composants. Un théme Retro arcade Cyberpunk minimaliste
    RetroArcade.ASPCore : Le Front-end MVC. Une interface utilisateur sombre, néon et dynamique, conçue pour l'immersion.
    RetroArcade.API : La couche de services exposant les endpoints sécurisés.
    RetroArcade.Domain : Le cœur métier (Entités, Logique de prix, Gestion des origines des machines).
    RetroArcade.DB : La couche Data optimisée. Utilisation d'ADO.NET pour un contrôle total sur les performances SQL.
    Tools.Cqs : Framework interne implémentant le pattern Command Query Separation.
    RetroArcade.Tests : Validation des fonctionnalités avec des tests XUnit.

```mermaid
graph TD
    %% 1. Déclaration des nœuds de la couche utilisateur
    A[Client MVC - Style Cyberpunk]
    B(RetroArcade.API)

    %% 2. Définition des subgraphs (Contenu uniquement)
    subgraph S1 [Handler CQS]
        D{RetroArcade.Domain Handler}
    end
    
    subgraph S2 [Logique d'Écriture]
        E[Commands]
        F[RetroArcade.DB - ADO.NET Execute]
    end

    subgraph S3 [Logique de Lecture]
        G[Queries]
        H[RetroArcade.DB - ADO.NET Reader]
    end

    I[(SQL Server)]

    %% 3. Déclaration des flux (Connexions)
    %% C'est ici que l'on définit l'ordre sans casser les boîtes
    A -- "Requête HTTPS + JWT" --> B
    B --> D
    
    %% Flux Commandes
    D --> E
    E --> F
    F --> I

    %% Flux Requêtes
    D --> G
    G --> H
    H --> I

    %% Style optionnel pour la clarté
    style D fill:#de2000cc,stroke:#333
    style I fill:#00d4ff,stroke:#333

```

⚡ Stack Technique

    Framework : ASP.NET Core MVC (Architecture Microservices).
    Data Access : ADO.NET (Performance brute, sans le surplus d'un ORM).
    Patterns : CQS (Command Query Separation) pour découpler les écritures des lectures.
    Sécurité : Authentification et Autorisation via JWT (JSON Web Tokens) + Refresh Token (coming soon).
    Design : UI Cyberpunk/Neon (CSS custom avec effets de scanlines et contrastes élevés).

🎮 Fonctionnalités par Profil
💎 Client (Gamer) (ongoing)

    Booking System : Réservation de bornes ou de salles privées (min. 4 personnes).
    Profil Tech : Historique des parties, notes sur les machines et consultation de la popularité "Live".

💼 Manager (Succursale) (ongoing)

    Mission Control : Gestion locale des salles, des machines et des disponibilités.
    Data Insights : Suivi des revenus et statistiques de fréquentation par plage horaire.

🛠️ RPA (Responsable des Parcs) (coming soon)

    Traçabilité Heritage : Historique complet des machines (Pays d'origine, année, déplacements inter-succursales).
    Analyse de Rentabilité : Calcul automatique de la rentabilité par rapport au prix d'achat et aux gains historiques.
    Customization : Gestion des catalogues de jeux pour les machines personnalisées.

💀 Admin (System) (ongoing)

    Full Override : Gestion complète des utilisateurs, des rôles et supervision globale de l'infrastructure.

🛠 Installation & Setup

    Prérequis : .NET SDK 8.0+ & SQL Server.
    Configuration : Modifier la chaîne de connexion dans RetroArcade.API/appsettings.json.


Partie 1 (implémentation des utilisateurs) (ongoing):

```mermaid
erDiagram
    ACCOUNT ||--|| ACCOUNT_CREDENTIAL : "possède"
    ACCOUNT ||--o{ BOOKING : "effectue"
    BUILDING ||--o{ ROOM : "contient"
    BUILDING ||--|| MANAGER : "est géré par"
    ROOM ||--o{ BOOKING : "est réservée"
    ROOM ||--o{ ROOM_FEEDBACK : "reçoit"
    ROOM ||--o{ ROOM_ARCADE_MACHINE : "héberge"
    ARCADE_MACHINE ||--o{ ROOM_ARCADE_MACHINE : "est installée dans"
    ARCADE_MACHINE }o--|| CATEGORIE : "appartient à"

    ACCOUNT {
        Guid AccountId PK
        Varchar Lastname
        Varchar Firstname
        Varchar Username
        Varchar Email
        Varchar Role
        Date CreationDate
        Date DisableDate
        Logical IsActive
    }

    ACCOUNT_CREDENTIAL {
        Guid AccountCredentialId PK
        Varbinary PasswordHash
        Guid Salt
        Guid AccountId FK
    }

    BOOKING {
        Guid BookingId PK
        DateTime BeginDate
        DateTime EndDate
        Int GroupSize
        Varchar Status
        Currency Price
        Guid RoomId FK
        Guid AccountId FK
    }

    ROOM {
        Guid RoomId PK
        Varchar Name
        Int Number
        Int MachineCapacity
        Currency Price
        Guid BuildingId FK
    }

    BUILDING {
        Guid BuildingId PK
        Varchar Name
        Time OpeningHour
        Time ClosingHour
        Varchar Street
        Varchar Number
        Varchar PostalCode
        Varchar City
        Varchar Country
        Guid ManagerId FK
    }

    MANAGER {
        Guid ManagerId PK
        Varchar Lastname
        Varchar Firstname
        Varchar Username
        Varchar Email
    }

    ROOM_FEEDBACK {
        Guid RoomFeedbackId PK
        Int Stars
        Date CommentDate
        Varchar Comment
        Varchar ClientUsername
        Guid RoomId FK
    }

    ROOM_ARCADE_MACHINE {
        Guid RoomId FK
        Guid ArcadeMachineId FK
        Varchar State
        DateTime InstallationDate
    }

    ARCADE_MACHINE {
        Guid ArcadeMachineId PK
        Varchar Name
        Varchar GameName
        Guid CategorieId FK
    }

    CATEGORIE {
        Guid CategorieId PK
        Varchar Name
    }
```
<img width="1375" height="675" alt="image" src="https://github.com/user-attachments/assets/8b50e36d-c5cd-44dd-9f2c-33f28b6c2428" />

Partie 2 (implémentation des managers) (coming soon):

Partie 3 (implémentation des RPA(Responsable des Parcs)) (coming soon):
