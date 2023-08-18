CREATE TABLE [dbo].[EntryPoint]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(20) NOT NULL,
	CONSTRAINT [UC_EntryPoint_Name] UNIQUE      ([Name])
)
