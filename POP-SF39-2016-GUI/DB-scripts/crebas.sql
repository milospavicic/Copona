-- crebas.sql
CREATE TABLE TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	Obrisan BIT,
);

CREATE TABLE Namestaj(
	Id INT PRIMARY KEY IDENTITY(1,1),
	TipNamestajaId INT,
	Naziv VARCHAR(100),
	Sifra VARCHAR(20),
	Cena NUMERIC(9,2),
	Kolicina INT,
	Obrisan BIT,
	FOREIGN KEY (TipNamestajaId) REFERENCES TipNamestaja(Id)
);
CREATE TABLE DodatnaUsluga(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	Cena NUMERIC(9,2),
	Obrisan BIT,
);
CREATE TABLE Korisnik(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Ime VARCHAR(30),
	Prezime VARCHAR(30),
	KorisnickoIme VARCHAR(30),
	Lozinka VARCHAR(30),
	TipKorisnika INT check (TipKorisnika in(1,2)),
	Obrisan BIT,
);
CREATE TABLE JedinicaProdaje(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProdajaId INT,
	NamestajId INT,
	Kolicina INT,
	Cena NUMERIC(9,2),
	Obrisan BIT
);
CREATE TABLE Akcija(
	IdAkcije INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	DatumPocetak DATE,
	DatumKraj DATE,
	Obrisan BIT
);
CREATE TABLE NaAkciji(
	IdNaAkciji INT PRIMARY KEY IDENTITY(1,1),
	IdNamestaja INT REFERENCES Namestaj(Id),
	IdAkcije INT REFERENCES Akcija(IdAkcije),
	Popust INT check(Popust<100 AND Popust>0),
	Obrisan BIT
);
CREATE TABLE Salon(
	  IdSalona INT PRIMARY KEY IDENTITY(1,1),
	  Naziv VARCHAR(30),
	  Adresa VARCHAR(60),
	  BrojTelefona VARCHAR(30),
	  Email VARCHAR(30),
	  WebAdresa VARCHAR(60),
	  BrRacuna VARCHAR(30),
	  Pib INT,
	  MaticniBr INT,
	  Obrisan BIT
);
CREATE TABLE ProdajaNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Kupac VARCHAR(30),
	BrRacuna VARCHAR(60),
	DatumProdaje DATETIME,
	UkupnaCena NUMERIC(9,2),
	Obrisan BIT
);
CREATE TABLE ProdataDodatnaUsluga(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProdajaId INT,
	DodatnaUslugaId INT,
	Obrisan BIT
);
DROP TABLE Akcija
DROP TABLE NaAkciji
DROP TABLE Namestaj
DROP TABLE ProdataDodatnaUsluga
DROP TABLE JedinicaProdaje
Drop TaBLE TipNamestaja
DROP TABLE NAMESTAJ
DROP TABLE KORISNIK


SELECT * FROM NaAkciji na INNER JOIN Akcija a ON na.IdAkcije = a.IdAkcije WHERE na.Obrisan = 0 and na.IdNamestaja = 2  AND DatumKraj>='2017.12.31'

UPDATE TipNamestaja SET Obrisan = 0 where Id = 8
UPDATE TipNamestaja SET Obrisan = 0 where Id = 3
UPDATE NAMESTAj SET Obrisan =0
UPDATE NAMESTAj SET TipNamestajaID = 3 WHERE TipNamestajaId = 1
SELECT * FROM NaAkciji na INNER JOIN Akcija a ON na.IdAkcije = a.IdAkcije WHERE na.Obrisan = 0 AND na.IdAkcije= 1 AND DatumKraj>='2017.1.1'

SELECT * FROM Akcija WHERE Obrisan=0 AND DatumPocetak<='12/3/2016' AND DatumKraj>='12/3/2016' OR Naziv like '12/3/2016'
SELECT * FROM Namestaj WHERE Cena like 5000

SELECT * FROM ProdajaNamestaja WHERE UkupnaCena = '25.12.2017' OR DatumProdaje like '25.12.2017' OR Kupac like '25.12.2017'

SELECT * FROM Namestaj WHERE 'CR' IN(Naziv,Cena,Sifra,TipNamestajaId)
SELECT * FROM Namestaj WHERE Cena = 5000

SELECT * FROM Namestaj WHERE Obrisan=0 AND Naziv LIKE '5000' OR Sifra LIKE '5000' OR Cena LIKE '5000' OR Kolicina LIKE '5000' OR TipNamestajaId LIKE '5000'

Select * FROM Akcija WHERE DatumPocetak like '2016-01-01'

SELECT * FROM Namestaj n INNER JOIN TipNamestaja tn ON n.TipNamestajaId = tn.Id WHERE n.Obrisan=0 AND (n.Naziv LIKE 'sto' OR Sifra LIKE 'sto' or tn.Naziv LIKE 'sto')

SELECT * FROM AKCIJA WHERE (IdAkcije IN (SELECT IdAkcije FROM NaAkciji WHERE IdNamestaja IN (SELECT Id FROM NAMESTAJ WHERE Naziv LIKE '%')) OR Naziv LIKE '%') AND Obrisan=0
SELECT * FROM Namestaj WHERE Id = @Id

SELECT * FROM ProdajaNamestaja WHERE (Id IN(SELECT ProdajaId FROM JedinicaProdaje WHERE NamestajId IN (SELECT Id FROM Namestaj WHERE Naziv LIKE '%'))OR Kupac LIKE '%' OR BrRacuna LIKE '%') AND Obrisan=0


SELECT * FROM ProdajaNamestaja WHERE (Id IN(SELECT ProdajaId FROM JedinicaProdaje WHERE NamestajId IN (SELECT Id FROM Namestaj WHERE Naziv LIKE '%')))
UPDATE ProdajaNamestaja SET Obrisan=0 WHERE Id=1004