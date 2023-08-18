CREATE PROCEDURE [dbo].[Sp_GetExtendedParkingInfo]
(
	@PlateNumber 	varchar(20)
)
AS

  
BEGIN


	SELECT VPS.[Id]
      ,VPS.[VehicleId]
      ,VPS.[ParkingSlotId]
      ,VPS.[EntryDateTime]
      ,VPS.[ExitDateTime]
      ,V.[PlateNumber]
      ,V.[Size] AS [VehicleSize]
      ,CASE 
			WHEN V.[Size] = 0 THEN 'SMALL'
			WHEN V.[Size] = 1 THEN 'MEDIUM'
			WHEN V.[Size] = 2 THEN 'LARGE'
			ELSE ''
		END AS [VehicleSizeName]
      ,PS.[IsOccupied]
      ,PS.[Size] AS [SlotSize]
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
	        WHERE psepd.ParkingSlotId = VPS.[ParkingSlotId] FOR XML PATH ('')), 1, 1, '')
        ) 'Coordinates'
  FROM [dbo].[VehicleParkingSlot] AS VPS
  LEFT JOIN [dbo].[Vehicle] AS V ON (V.[Id] = VPS.[VehicleId])
  LEFT JOIN [dbo].[ParkingSlot] AS PS ON (PS.[Id] = VPS.[ParkingSlotId])
  WHERE V.[PlateNumber] = @PlateNumber
  ORDER BY VPS.[ExitDateTime] DESC
END
GO
