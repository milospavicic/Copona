-- crebas.sql
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
CREATE TABLE TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	Obrisan BIT,
);
CREATE TABLE DodatnaUsluga(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	Cena NUMERIC(11,2),
	Obrisan BIT,
);
CREATE TABLE Namestaj(
	Id INT PRIMARY KEY IDENTITY(1,1),
	TipNamestajaId INT REFERENCES TipNamestaja(Id),
	Naziv VARCHAR(100),
	Sifra VARCHAR(20),
	Cena NUMERIC(11,2),
	Kolicina INT,
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
CREATE TABLE ProdajaNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Kupac VARCHAR(30),
	BrRacuna VARCHAR(60),
	DatumProdaje DATETIME,
	UkupnaCena NUMERIC(11,2),
	Obrisan BIT
);
CREATE TABLE ProdataDodatnaUsluga(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProdajaId INT REFERENCES ProdajaNamestaja(Id),
	DodatnaUslugaId INT REFERENCES DodatnaUsluga(Id),
	Obrisan BIT
);
CREATE TABLE JedinicaProdaje(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProdajaId INT REFERENCES ProdajaNamestaja(Id),
	NamestajId INT REFERENCES Namestaj(Id),
	Kolicina INT,
	Cena NUMERIC(11,2),
	Obrisan BIT
);
CREATE TABLE Akcija(
	IdAkcije INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(100),
	DatumPocetak DATETIME,
	DatumKraj DATETIME,
	Obrisan BIT
);
CREATE TABLE NaAkciji(
	IdNaAkciji INT PRIMARY KEY IDENTITY(1,1),
	IdNamestaja INT REFERENCES Namestaj(Id),
	IdAkcije INT REFERENCES Akcija(IdAkcije),
	Popust INT check(Popust<100 AND Popust>0),
	Obrisan BIT
);



DROP TABLE ProdataDodatnaUsluga
DROP TABLE JedinicaProdaje
DROP TABLE ProdajaNamestaja
DROP TABLE NaAkciji
Drop TaBLE Akcija
DROP TABLE Korisnik
DROP TABLE Salon
DROP TABLE Namestaj
DROP TABLE TipNamestaja
DROP TABLE DodatnaUsluga
