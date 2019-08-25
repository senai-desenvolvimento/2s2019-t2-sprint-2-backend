-- CRIAR BASE DE DADOS
CREATE DATABASE Gufos;

-- COLOCAR O BANCO DE DADOS EM USO
USE Gufos;

-- TABELA DE CATEGORIAS DOS EVENTOS
CREATE TABLE  Categorias 
(  
    IdCategoria     INT PRIMARY KEY NOT NULL IDENTITY
    ,Nome           VARCHAR(200) NOT NULL UNIQUE
);

-- TABELA DE USUARIOS
CREATE TABLE Usuarios
(
    IdUsuario     INT PRIMARY KEY NOT NULL IDENTITY
    ,Nome         VARCHAR(250) NOT NULL
    ,Email        VARCHAR(250) UNIQUE NOT NULL
    ,Senha        VARCHAR(250) NOT NULL
    ,Permissao    VARCHAR(200) NOT NULL
);

-- TABELA DE EVENTOS
CREATE TABLE Eventos 
(
    IdEvento        INT PRIMARY KEY NOT NULL IDENTITY
    ,Titulo         VARCHAR(200) NOT NULL
    ,Descricao      TEXT
    ,DataEvento     DATETIME NOT NULL
    ,Ativo          BIT NOT NULL DEFAULT(1)
    ,Localizacao    VARCHAR(250) NULL
    ,IdCategoria    INT FOREIGN KEY REFERENCES Categorias (IdCategoria)
);

-- TABELA DE PRESENCAS
CREATE TABLE Presencas
(
    IdUsuario   INT FOREIGN KEY REFERENCES Usuarios (IdUsuario)
    ,IdEvento   INT FOREIGN KEY REFERENCES Eventos (IdEvento)
);

USE Gufos;

-- inserir na tabela de usu�rios
INSERT INTO Usuarios (Nome, Email, Senha, Permissao) VALUES ('Administrador', 'admin@admin.com', '123456', 'ADMINISTRADOR');

INSERT INTO Usuarios (Nome, Email, Senha, Permissao) VALUES ('A', 'a@aluno.com', '123456', 'ALUNO'), ('B', 'b@aluno.com', '123456', 'ALUNO');

-- inserir na tabela de categorias
INSERT INTO Categorias VALUES ('Jogos'), ('Meetup'), ('Futebol');

-- inserir na tabela de eventos
INSERT INTO Eventos (Titulo, Descricao, DataEvento, Ativo, Localizacao, IdCategoria) VALUES ('Titulo A', 'Descricao A', GETDATE(), 1, 'Alameda Barao de Limeira, 539', 1);
INSERT INTO Eventos (Titulo, Descricao, DataEvento, Ativo, Localizacao, IdCategoria) VALUES ('Titulo B', 'Campeonato de Futebol', GETDATE(), 1, 'Alameda Barao de Limeira, 539', 3);

-- inserir na tabela de presen�as
INSERT INTO Presencas (IdUsuario, IdEvento) VALUES (2, 1), (2, 2), (3, 1);

-- Colocando o banco de dados em uso
USE Gufos;

-- Selecionando os itens separadamente de cada tabela
SELECT * FROM Categorias;
SELECT * FROM Eventos;
SELECT * FROM Usuarios;
SELECT * FROM Presencas;

-- Selecionando os eventos e suas respectivas categorias
SELECT E.*, C.* FROM Eventos E INNER JOIN Categorias C ON E.IdCategoria = C.IdCategoria;

-- Selecionando os usu�rios, os eventos e as presen�as
SELECT P.*, U.*, E.* FROM Presencas P INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario INNER JOIN Eventos E ON P.IdEvento = E.IdEvento;

-- Selecionando as presencas, os usuarios, quais eventos eles irao participar e quais as respectivas categorias
SELECT P.*, U.*, E.*, C.* FROM Presencas P INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario INNER JOIN Eventos E ON P.IdEvento = E.IdEvento INNER JOIN Categorias C ON E.IdCategoria = C.IdCategoria;
