CREATE TABLE [dbo].[ParkingSlotEntryPointDistance]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParkingSlotId] INT NOT NULL, 
    [EntryPointId] INT NOT NULL, 
    [Distance] INT NOT NULL, 
    CONSTRAINT [FK_ParkingSlotEntryPointDistance_ParkingSlot] FOREIGN KEY ([ParkingSlotId]) REFERENCES [ParkingSlot]([Id]),
    CONSTRAINT [FK_ParkingSlotEntryPointDistance_EntryPoint] FOREIGN KEY ([EntryPointId]) REFERENCES [EntryPoint]([Id])
)
