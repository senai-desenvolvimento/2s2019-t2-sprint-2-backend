CREATE DATABASE AutoPecas
GO

USE AutoPecas
GO

CREATE TABLE Usuarios
(
	UsuarioId		INT PRIMARY KEY IDENTITY
	, Email			VARCHAR(255) NOT NULL UNIQUE
	, Senha			VARCHAR(255) NOT NULL
);

CREATE TABLE Fornecedores 
(
	FornecedorId	INT PRIMARY KEY IDENTITY
	, CNPJ			CHAR(14)		NOT NULL UNIQUE
	, RazaoSocial	VARCHAR(255)	NOT NULL UNIQUE
	, NomeFantasia	VARCHAR(255)	NOT NULL
	, Endereco		VARCHAR(255)	NOT NULL
	, UsuarioId		INT FOREIGN KEY REFERENCES Usuarios(UsuarioId) NOT NULL UNIQUE
);

CREATE TABLE Pecas (
	PecaId			INT PRIMARY KEY IDENTITY
	, Codigo		VARCHAR(255) NOT NULL UNIQUE
	, Descricao		TEXT
	, Peso			FLOAT(2) NOT NULL
	, PrecoCusto	FLOAT(2) NOT NULL
	, PrecoVenda	FLOAT(2) NOT NULL
	, FornecedorId	INT FOREIGN KEY REFERENCES Fornecedores(FornecedorId) NOT NULL
);

-- USUARIOS
SELECT * FROM Usuarios;
INSERT INTO Usuarios (Email, Senha) VALUES ('fa@email.com', '123456'), ('fb@email.com', '123456');

-- FORNECEDORES
SELECT * FROM Fornecedores;
INSERT INTO Fornecedores (CNPJ, RazaoSocial, NomeFantasia, Endereco, UsuarioId) VALUES ('12345678901234', 'Fornecedor A', 'Fornecedor A', 'Alameda Barão de Limeira', 1)
																					  ,('12345678954321', 'Fornecedor B', 'Fornecedor B', 'Alameda Barão de Limeira', 2);

-- PECAS
SELECT * FROM Pecas
GO
INSERT INTO Pecas (Codigo, Descricao, Peso, PrecoCusto, PrecoVenda, FornecedorId) 
	VALUES ('A1', 'Descricao Peca A', 1, 20, 40, 3)
	,('B1', 'Descricao Peca B', 2, 20, 40, 3)
	,('C1', 'Descricao Peca C', 3, 40, 90, 4)
	,('D1', 'Descricao Peca D', 4, 30, 40, 4);