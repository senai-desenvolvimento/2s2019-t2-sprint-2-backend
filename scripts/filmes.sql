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