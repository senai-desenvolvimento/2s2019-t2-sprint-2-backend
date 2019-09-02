CREATE DATABASE Ekips;

USE Ekips;

CREATE TABLE Departamentos (
	DepartamentoId 		INT PRIMARY KEY IDENTITY
	, Nome 				VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Cargos (
	CargoId 	INT PRIMARY KEY IDENTITY
	, Nome 		VARCHAR(255) NOT NULL UNIQUE
	, Ativo 	BIT
);

CREATE TABLE Usuarios (
	UsuarioId		INT PRIMARY KEY IDENTITY
	, Email			VARCHAR(255) NOT NULL UNIQUE
	, Senha			VARCHAR(255) NOT NULL
	, Permissao		VARCHAR(20) NOT NULL
);

CREATE TABLE Funcionarios (
	FuncionarioId		INT PRIMARY KEY IDENTITY
	, Nome				VARCHAR(255) NOT NULL
	, CPF				CHAR(11) UNIQUE
	, DataNascimento	DATE
	, Salario			MONEY
	, DepartamentoId	INT FOREIGN KEY REFERENCES Departamentos(DepartamentoId)
	, CargoId			INT FOREIGN KEY REFERENCES Cargos(CargoId)
	, UsuarioId			INT UNIQUE FOREIGN KEY REFERENCES Usuarios(UsuarioId)
);

INSERT INTO Departamentos (Nome) VALUES ('Departamento Jurídico');
INSERT INTO Departamentos (Nome) VALUES ('Design');
INSERT INTO Departamentos (Nome) VALUES ('Tecnologia');

INSERT INTO Cargos (Nome, Ativo) VALUES ('Desenvolvedor', 1);
INSERT INTO Cargos (Nome, Ativo) VALUES ('Designer UI/UX', 1);

INSERT INTO Usuarios (Email, Senha, Permissao) VALUES ('admin@email.com', '123456', 'ADMINISTRADOR');
INSERT INTO Usuarios (Email, Senha, Permissao) VALUES ('comum@email.com', '123456', 'COMUM');

INSERT INTO Funcionarios (Nome, CPF, DataNascimento, Salario, DepartamentoId, CargoId, UsuarioId)
VALUES ('Líder', '12345678901', '18-03-1993', 10000, 3, 1, 1);

INSERT INTO Funcionarios (Nome, CPF, DataNascimento, Salario, DepartamentoId, CargoId, UsuarioId)
VALUES ('Comumzinho', '12345678910', '18-03-1993', 9000, 2, 2, 2);

SELECT * FROM Departamentos;
SELECT * FROM Cargos;
SELECT * FROM Usuarios;
SELECT * FROM Funcionarios;