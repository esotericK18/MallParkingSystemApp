CREATE TABLE [dbo].[VehicleParkingSlot]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VehicleId] INT NOT NULL, 
    [ParkingSlotId] INT NOT NULL, 
    [EntryDateTime] DATETIME NOT NULL, 
    [ExitDateTime] DATETIME NULL, 
    CONSTRAINT [FK_VehicleParkingSlot_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle]([Id]),
    CONSTRAINT [FK_VehicleParkingSlot_ParkingSlot] FOREIGN KEY ([ParkingSlotId]) REFERENCES [ParkingSlot]([Id])

)
