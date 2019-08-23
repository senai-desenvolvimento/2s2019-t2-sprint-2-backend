CREATE DATABASE Peoples;

USE Peoples;

CREATE TABLE Funcionarios 
(
	IdFuncionario	INT IDENTITY PRIMARY KEY
	,Nome			VARCHAR(200) NOT NULL
	,Sobrenome		VARCHAR(255)
);
GO

-- apos a alteracao

ALTER TABLE Funcionarios
ADD DataNascimento DATE