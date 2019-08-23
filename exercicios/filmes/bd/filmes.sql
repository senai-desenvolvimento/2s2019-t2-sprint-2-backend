CREATE DATABASE RoteiroFilmes;

USE RoteiroFilmes;

CREATE TABLE Generos 
(
    IdGenero    INT PRIMARY KEY IDENTITY
    ,Nome       VARCHAR(200) NOT NULL UNIQUE
);

CREATE TABLE Filmes
(
    IdFilme     INT PRIMARY KEY IDENTITY
    ,Titulo     VARCHAR(200) UNIQUE
    ,IdGenero   INT FOREIGN KEY REFERENCES Generos (IdGenero)
);

INSERT INTO Generos (Nome) VALUES ('Romance'), ('Drama');
INSERT INTO Filmes (Titulo, IdGenero) VALUES ('Exterminador do Futuro', 1);

SELECT IdGenero, Nome FROM Generos;
SELECT IdFilme, Titulo, IdGenero FROM Filmes;
SELECT F.IdFilme, F.Titulo, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero;