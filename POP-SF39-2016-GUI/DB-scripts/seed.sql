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

INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(1,'Ultra polica','UL1PO',123.5,2,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(2,'Crni krevet','CR2KR',5000,22,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(3,'Sto 123','ST11ST',2000,13,0)
INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Cena,Kolicina,Obrisan)
VALUES(4,'Lazy bag','LA3ST',6700,17,0)