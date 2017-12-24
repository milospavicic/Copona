-- crebas.sql
CREATE TABLE TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	Obrisan BIT,

);
GO
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
	TipKorisnika VARCHAR(20) check (TipKorisnika in('Administrator','Prodavac')),
	Obrisan BIT,
);
CREATE TABLE JedinicaProdaje(
	Id INT PRIMARY KEY IDENTITY(1,1),
	NamestajId INT,
	Kolicina INT,
	Obrisan BIT
);
CREATE TABLE Akcija(
	IdAkcije INT PRIMARY KEY IDENTITY(1,1),
	DatumPocetak DATE,
	DatumKraj DATE,
	Obrisan BIT
);
CREATE TABLE NaAkciji(
	IdNaAkciji INT PRIMARY KEY IDENTITY(1,1),
	IdNamestaja INT REFERENCES Namestaj(Id),
	IdAkcije INT REFERENCES Akcija(IdAkcije),
	Popust INT check(Popust<=100),
	Obrisan BIT
);
drop table NaAkciji
