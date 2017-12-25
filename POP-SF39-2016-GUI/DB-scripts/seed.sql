--seed.sql
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
----------------------------------------------------------
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(1,'Ultra polica','UL1PO',123.5,2,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(2,'Crni krevet','CR2KR',5000,22,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(3,'Sto 123','ST11ST',2000,13,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(4,'Lazy bag','LA3ST',6700,17,0)
----------------------------------------------------------
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Prevoz',2000,0)
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Pakovanje',400,0)
INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan)
VALUES('Sastavljanje',250,0)
----------------------------------------------------------
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Zoran','Jovanovic','123','123','Administrator',0)
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Goran','Jovanovic','1234','1234','Prodavac',0)
INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan)
VALUES('Jovan','Jovanovic','321','321','Administrator',0)
----------------------------------------------------------
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan)
VALUES(1,1,2,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan)
VALUES(1,2,2,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan)
VALUES(2,3,2,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan)
VALUES(2,4,2,0)
INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan)
VALUES(2,5,2,0)
----------------------------------------------------------
INSERT INTO Akcija(DatumPocetak,DatumKraj,Obrisan)
VALUES('1/1/2016','2/2/2017',0)
INSERT INTO Akcija(DatumPocetak,DatumKraj,Obrisan)
VALUES('12/2/2016','3/13/2018',0)
INSERT INTO Akcija(DatumPocetak,DatumKraj,Obrisan)
VALUES('4/7/2016','9/3/2017',0)
INSERT INTO Akcija(DatumPocetak,DatumKraj,Obrisan)
VALUES('11/11/2016','9/3/2017',0)
----------------------------------------------------------
INSERT INTO NaAkciji(IdNamestaja,IdAkcije,Popust,Obrisan)
VALUES(1,1,30,0)
INSERT INTO NaAkciji(IdNamestaja,IdAkcije,Popust,Obrisan)
VALUES(2,1,30,0)
INSERT INTO NaAkciji(IdNamestaja,IdAkcije,Popust,Obrisan)
VALUES(4,1,30,0)
----------------------------------------------------------
INSERT INTO Salon(Naziv,Adresa,BrojTelefona,Email,WebAdresa,BrRacuna,Pib,MaticniBr,Obrisan)
VALUES('Moj Salon','Kralja Petra II, br 41','060-553/2313','example@mojsalon.com','www.mojsalon.com','58267581764',123456789,987654321,0)
----------------------------------------------------------
INSERT INTO ProdajaNamestaja(Kupac,BrRacuna,DatumProdaje,Obrisan)
VALUES('Zoran','12131415','2017/12/25',0)
INSERT INTO ProdajaNamestaja(Kupac,BrRacuna,DatumProdaje,Obrisan)
VALUES('Goran','51413121','2017/12/24',0)
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