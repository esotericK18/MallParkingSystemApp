﻿CREATE TABLE [dbo].[ParkingSlot]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Size] INT NOT NULL, 
    [IsOccupied] BIT NOT NULL DEFAULT 0
)
