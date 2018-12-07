CREATE TABLE Species(
	[ID] int not null primary key identity(1,1),
	[Name] nvarchar(50) not null
);

ALTER TABLE Animal
	ADD [SpeciesID] int;

ALTER TABLE Animal 
    ADD CONSTRAINT [FK_Animal_Species] FOREIGN KEY (SpeciesID) REFERENCES Species(ID);