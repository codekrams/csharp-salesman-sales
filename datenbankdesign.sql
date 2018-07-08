create database vertretermanagement if not exist;

create table vertreter ( 
id integer primary key auto_increment,
nname varchar(100) not null,
vname varchar (100) not null);

create table auftraege (
id integer primary key auto_increment,
auftragsnummer varchar(100) not null,
vertreter_id integer not null,
umsatz decimal not null,
FOREIGN KEY (vertreter_id) REFERENCES vertreter(id)
);

insert into vertreter
values (NULL, "Schulze", "Maria"),
(NULL, "Merten", "Josef"),
(NULL, "Hermann", "Rolf");

insert into auftraege
values (NULL, "A-201103-01627", 3, 43289.35),
(NULL, "A-201103-01845", 3, 50212.51),
(NULL, "A-201103-01856", 3, 70212.45),
(NULL, "A-201103-01989", 3, 5521.67),
(NULL, "A-201103-01990", 2, 5521.89),
(NULL, "A-201103-01878", 2, 43289.51),
(NULL, "A-201103-01633", 2, 73289.89),
(NULL, "A-201103-12856", 1, 43289.23),
(NULL, "A-201103-12890", 1, 100289.56),
(NULL, "A-201103-12888", 1, 13289.00),
(NULL, "A-201103-12856", 1, 11289.25),
(NULL, "A-201103-12856", 1, 33289.51);

select auftraege.auftragsnummer, auftraege.umsatz, vertreter.nname
from auftraege, vertreter
where vertreter.id=auftraege.vertreter_id;

update auftraege set umsatz = 50212.51 where id = 2;

alter table auftraege
modify column umsatz decimal(10,2); 

