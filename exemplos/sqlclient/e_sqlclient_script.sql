-- EXEMPLO SQLCLIENT

-- criar a base de dados
CREATE DATABASE E_SqlClient
GO

-- coloca-la em uso
USE E_SqlClient
GO

-- criar a tabela de categorias
CREATE TABLE Categorias 
(
	CategoriaId		INT PRIMARY KEY IDENTITY
	,Nome			VARCHAR(250) UNIQUE
	,DataCriacao	DATETIME
	,Ativo			BIT
)
GO

-- inserir alguns registros de exemplo
INSERT INTO Categorias (Nome, DataCriacao, Ativo) 
			VALUES		('Categoria A', GETDATE(), 1)
						,('Categoria B', GETDATE(), 0)
GO

-- selecionar os registros inseridos
SELECT CategoriaId, Nome, DataCriacao, Ativo FROM Categorias
GO

SELECT CategoriaId, Nome, DataCriacao, Ativo 
FROM Categorias
WHERE Ativo = 1
GO

SELECT CategoriaId, Nome, DataCriacao, Ativo 
FROM Categorias
WHERE CategoriaId = 1
GO

SELECT CategoriaId, Nome, DataCriacao, Ativo 
FROM Categorias
WHERE Nome LIKE '%Cat%'
GO

UPDATE Categorias
SET Nome = 'Categoria A', Ativo = 1
WHERE CategoriaId = 1;