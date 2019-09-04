CREATE DATABASE CgStore
GO

USE CgStore
GO

CREATE TABLE Permissoes
(
	PermissaoId		INT IDENTITY PRIMARY KEY
	,Nome			VARCHAR(100) NOT NULL UNIQUE
)
GO

INSERT INTO Permissoes (Nome) VALUES ('Administrador'), ('Comum');

SELECT PermissaoId, Nome FROM Permissoes
GO

CREATE TABLE Usuarios 
(
	UsuarioId		INT IDENTITY PRIMARY KEY
	,Email			VARCHAR(250) NOT NULL UNIQUE
	,Senha			VARCHAR(255) NOT NULL
	,PermissaoId	INT FOREIGN KEY REFERENCES Permissoes (PermissaoId)
)
GO

INSERT INTO Usuarios (Email, Senha, PermissaoId) VALUES ('admin@email.com', '123456', 1), ('cliente@email.com', '123456', 2);
INSERT INTO Usuarios (Email, Senha, PermissaoId) VALUES ('administrador@email.com', '123456', 1);

SELECT UsuarioId, Email, Senha, PermissaoId FROM Usuarios
GO

CREATE TABLE Categorias
(
	CategoriaId		INT IDENTITY PRIMARY KEY
	,Nome			VARCHAR(150) NOT NULL UNIQUE
	,DataCriacao	DATETIME DEFAULT(GETDATE())
	,UsuarioId		INT FOREIGN KEY REFERENCES Usuarios (UsuarioId)
)
GO

INSERT INTO Categorias (Nome, UsuarioId) VALUES ('Categoria A', 1), ('Categoria B', 1), ('Categoria C', 3);
GO

SELECT CategoriaId, Nome, DataCriacao, UsuarioId FROM Categorias
GO