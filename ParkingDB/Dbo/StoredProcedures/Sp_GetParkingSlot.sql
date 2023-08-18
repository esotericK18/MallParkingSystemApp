CREATE PROCEDURE [dbo].[Sp_GetParkingSlot]
(
	@EntryPoint 	int
	,@VehicleId 	int
)
AS

DECLARE @VehicleSize int = 1
DECLARE @ParkingSlotID int = 0

BEGIN

	--get the size of the vehicle
	SELECT @VehicleSize = [Size] FROM [dbo].[Vehicle] WHERE Id = @VehicleId
	
	--Get available parking slot id
	SELECT TOP 1
		 D.[ParkingSlotId]
		,D.[EntryPointId]
		,D.[Id] AS DistanceId
		,EP.[Name] AS EntryPointName 
		,D.[Distance]
		,V.[Size] AS [VehicleSize]
		,CASE 
			WHEN V.[Size] = 0 THEN 'SMALL'
			WHEN V.[Size] = 1 THEN 'MEDIUM'
			WHEN V.[Size] = 2 THEN 'LARGE'
			ELSE ''
		END AS [VehicleSizeName]
        ,PS.[Size] AS SlotSize
		,CASE 
			WHEN PS.[Size] = 0 THEN 'SMALL'
			WHEN PS.[Size] = 1 THEN 'MEDIUM'
			WHEN PS.[Size] = 2 THEN 'LARGE'
			ELSE ''
		END AS [SlotSizeName]
        ,(STUFF((
	        SELECT ', ' + CAST(psepd.Distance AS VARCHAR(10))
	        FROM ParkingSlotEntryPointDistance psepd
	        INNER JOIN ParkingSlot ps ON psepd.ParkingSlotId = ps.Id
	        WHERE psepd.ParkingSlotId = D.[ParkingSlotId] FOR XML PATH ('')), 1, 1, '')
        ) 'Coordinates'
		,PS.[IsOccupied]
	FROM [dbo].[ParkingSlotEntryPointDistance] AS D
	LEFT JOIN [dbo].[ParkingSlot] AS PS ON (PS.[Id] = D.[ParkingSlotId])
	LEFT JOIN [dbo].[EntryPoint] AS EP ON (EP.[Id] = D.[EntryPointId])
	LEFT JOIN [dbo].[VehicleParkingSlot] AS VPS ON (VPS.[ParkingSlotId] = D.[ParkingSlotId])
	LEFT JOIN [dbo].[Vehicle] AS V ON (VPS.[VehicleId] = V.[Id])
	
	WHERE 
	  D.[EntryPointId] = @EntryPoint 
	  AND PS.[IsOccupied] = 0
	  AND PS.[Size] >= @VehicleSize
	ORDER BY D.[Distance], PS.[Size]  ASC
END
GO
