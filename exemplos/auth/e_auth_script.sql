-- criar banco de dados 
CREATE DATABASE E_Auth
GO

-- coloca-lo em uso
USE E_Auth
GO

-- criar tabela de permissoes
CREATE TABLE Permissoes 
(
	PermissaoId		INT PRIMARY KEY IDENTITY
	,Nome			VARCHAR(255)
)
GO

-- criar tabela de usuarios
CREATE TABLE Usuarios 
(
	UsuarioId		INT PRIMARY KEY IDENTITY
	,Email			VARCHAR(255) NOT NULL UNIQUE
	,Senha			VARCHAR(255) NOT NULL
	,PermissaoId	INT FOREIGN KEY REFERENCES Permissoes (PermissaoId)
)
GO

-- inserir as permissoes
INSERT INTO Permissoes (Nome) VALUES ('Administrador'), ('Comum');

-- inserir os usuarios
INSERT INTO Usuarios (Email, Senha, PermissaoId)
VALUES ('admin@email.com', '123456', 1)
	   ,('comum@email.com', '123456', 2);
GO

-- selecionar as permissoes
SELECT PermissaoId, Nome FROM Permissoes
GO

-- selecionar os usuarios
SELECT UsuarioId, Email, Senha, PermissaoId FROM Usuarios
GO

-- selecionar os usuarios e suas respectivas permissoes
SELECT U.UsuarioId, U.Email, U.Senha, P.PermissaoId, P.Nome FROM Usuarios U
INNER JOIN Permissoes P
ON U.PermissaoId = P.PermissaoId
GO