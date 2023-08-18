CREATE TABLE [dbo].[Vehicle]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlateNumber] VARCHAR(20) NOT NULL, 
    [Size] INT NOT NULL, 
    CONSTRAINT [UC_Vehicle_Name] UNIQUE      ([PlateNumber])
)
