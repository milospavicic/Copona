--seed.sql
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Ostalo',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Polica',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Krevet',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Sto',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Stolica',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Dvosed',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Trosed',0);
INSERT INTO TipNamestaja(Naziv,Obrisan)
VALUES('Lampa',0);
----------------------------------------------------------
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(2,'Ultra polica','UL1PO',1200.5,52,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(3,'Crni krevet','CR2KR',5000,22,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(4,'Sto 123','ST11ST',2000,135,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(5,'Lazy bag black','LA44ST',6700,157,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(3,'Leteci krevet','LE1PO',12300.5,12,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(3,'Vodeni krevet','VO2KR',2300,32,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(4,'Sto 123','ST11ST',1500,53,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(5,'Lazy bag','LA3ST',3700,27,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(7,'Crni trosed','CR3TR',6700,27,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(6,'Crni dvosed','CR31DV',4500,123,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(4,'Pisaci sto','PI22ST',4200,123,0)
----------------------------------------------------------
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Prevoz',2000,0)
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Pakovanje',400,0)
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Sastavljanje',250,0)
----------------------------------------------------------
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Zoran','Jovanovic','123','123',1,0)
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Goran','Jovanovic','1234','1234',2,0)
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Jovan','Jovanovic','321','321',1,0)
----------------------------------------------------------
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Cena,Obrisan)
VALUES(1,1,2,123.5,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Cena,Obrisan)
VALUES(1,2,2,5000,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Cena,Obrisan)
VALUES(2,3,2,2000,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Cena,Obrisan)
VALUES(2,4,2,5025,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Cena,Obrisan)
VALUES(2,5,2,8502.52,0)
----------------------------------------------------------
INSERT INTO Akcija(Naziv,DatumPocetak,DatumKraj,Obrisan)
VALUES('Novogodisnja akcija','1/1/2016','2/2/2018',0)
INSERT INTO Akcija(Naziv,DatumPocetak,DatumKraj,Obrisan)
VALUES('Zimska akcija','12/2/2016','3/13/2018',0)
INSERT INTO Akcija(Naziv,DatumPocetak,DatumKraj,Obrisan)
VALUES('Test','12/2/2016','3/13/2017',0)
----------------------------------------------------------
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(1,1,15,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(1,2,11,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(1,3,17,0)
--
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(2,4,25,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(2,5,31,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(2,6,47,0)
--
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(3,7,25,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(3,8,31,0)
INSERT INTO NaAkciji(IdAkcije,IdNamestaja,Popust,Obrisan)
VALUES(3,9,47,0)
----------------------------------------------------------
INSERT INTO Salon(Naziv,Adresa,BrojTelefona,Email,WebAdresa,BrRacuna,Pib,MaticniBr,Obrisan)
VALUES('Moj Salon','Kralja Petra II, br 41','060-553/2313','example@mojsalon.com','www.mojsalon.com','58267581764',123456789,987654321,0)
----------------------------------------------------------
INSERT INTO ProdajaNamestaja(Kupac,BrRacuna,DatumProdaje,UkupnaCena,Obrisan)
VALUES('Zoran','12131415','2017/12/25',50000,0)
INSERT INTO ProdajaNamestaja(Kupac,BrRacuna,DatumProdaje,UkupnaCena,Obrisan)
VALUES('Goran','51413121','2017/12/24',50000,0)
----------------------------------------------------------
INSERT INTO ProdataDodatnaUsluga(ProdajaId,DodatnaUslugaId,Obrisan)
VALUES(1,1,0)
INSERT INTO ProdataDodatnaUsluga(ProdajaId,DodatnaUslugaId,Obrisan)
VALUES(1,2,0)
INSERT INTO ProdataDodatnaUsluga(ProdajaId,DodatnaUslugaId,Obrisan)
VALUES(1,3,0)
-------------------------------------------------------------------------------
SELECT * FROM NaAkciji
SELECT DISTINCT POPUST FROM NaAkciji WHERE Obrisan = 0 AND IdAkcije = 1;

UPDATE NaAkciji SET Popust = 15 WHERE Obrisan = 0 and IdAkcije = 1
DELETE FROM NaAkciji
DROP TABLE NaAkciji

SELECT * FROM Namestaj WHERE Id not in (SELECT IdNamestaja FROM NaAkciji WHERE obrisan=0)
