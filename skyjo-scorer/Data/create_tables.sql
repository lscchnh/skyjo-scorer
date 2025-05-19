-- Création de la table Player
CREATE TABLE IF NOT EXISTS Player (
    Name TEXT PRIMARY KEY
);

-- Création de la table Game
CREATE TABLE IF NOT EXISTS Game (
    Number INTEGER PRIMARY KEY AUTOINCREMENT,
	PlayedOn TEXT NOT NULL DEFAULT (DATETIME('now'))
);

-- Création de la table PlayerScore
CREATE TABLE IF NOT EXISTS PlayerScore (
    PlayerName TEXT NOT NULL,
    GameNumber INTEGER NOT NULL,
    Score INTEGER NOT NULL,

    PRIMARY KEY (PlayerName, GameNumber),
    FOREIGN KEY (PlayerName) REFERENCES Player(Name) ON DELETE CASCADE,
    FOREIGN KEY (GameNumber) REFERENCES Game(Number) ON DELETE CASCADE
);