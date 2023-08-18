if not exists (select 1 from [dbo].[EntryPoint])
begin
	insert into [dbo].[EntryPoint] 
    ([Name])
	values 
	('A'),
	('B'),
	('C');
end


if not exists (select 1 from [dbo].[ParkingSlot])
begin
	insert into [dbo].[ParkingSlot] 
    ([Size])
	values 
	('0'),	-- SMALL
	('0'),	-- SMALL
	('0'),	-- SMALL
	('1'),	-- MEDIUM
	('1'),	-- MEDIUM
	('1'),	-- MEDIUM
	('2'),	-- LARGE
	('2'),	-- LARGE
	('2');	-- LARGE
end

if not exists (select 1 from [dbo].[ParkingSlotEntryPointDistance])
begin
	insert into [dbo].[ParkingSlotEntryPointDistance] 
    ([ParkingSlotId], [EntryPointId], [Distance])
	values 
	-------------------------------------------------------------
	--				SLOT SIZE	|	ENTRYPOINT	|	DISTANCE
	-------------------------------------------------------------
	(1, 1, 2), --	SMALL		|	A			|	2			|
	(2, 1, 1), --	SMALL		|	A			|	1			|
	(3, 1, 2), --	SMALL		|	A			|	2			|
	(1, 2, 2), --	SMALL		|		B		|		2		|
	(2, 2, 3), --	SMALL		|		B		|		3		|
	(3, 2, 4), --	SMALL		|		B		|		4		|
	(1, 3, 4), --	SMALL		|	C			|	4			|
	(2, 3, 3), --	SMALL		|	C			|	3			|
	(3, 3, 4), --	SMALL		|	C			|	4			|
	(4, 1, 3), --	MEDIUM		|		A		|		3		|
	(5, 1, 2), --	MEDIUM		|		A		|		2		|
	(6, 1, 3), --	MEDIUM		|		A		|		3		|
	(4, 2, 1), --	MEDIUM		|	B			|	1			|
	(5, 2, 2), --	MEDIUM		|	B			|	2			|
	(6, 2, 3), --	MEDIUM		|	B			|	3			|
	(4, 3, 3), --	MEDIUM		|		C		|		3		|
	(5, 3, 2), --	MEDIUM		|		C		|		2		|
	(6, 3, 3), --	MEDIUM		|		C		|		3		|
	(7, 1, 4), --	LARGE		|	A			|	4			|
	(8, 1, 3), --	LARGE		|	A			|	3			|
	(9, 1, 4), --	LARGE		|	A			|	4			|
	(7, 2, 2), --	LARGE		|		B		|		2		|
	(8, 2, 3), --	LARGE		|		B		|		3		|
	(9, 2, 4), --	LARGE		|		B		|		4		|
	(7, 3, 2), --	LARGE		|	C			|	2			|
	(8, 3, 1), --	LARGE		|	C			|	1			|
	(9, 3, 2); --	LARGE		|	C			|	2			|
end

if not exists (select 1 from [dbo].[Vehicle])
begin
	INSERT INTO [dbo].[Vehicle]
    ([PlateNumber], [Size])
    VALUES
    ('ABS-001',0)
    ,('ABS-002',0)
    ,('ABS-003',0)
    ,('ABM-001',1)
    ,('ABM-002',1)
    ,('ABM-003',1)
    ,('ABL-001',2)
    ,('ABL-002',2)
    ,('ABL-003',2)
end


if not exists (select 1 from [dbo].[VehicleParkingSlot])
begin
	INSERT INTO [MallParkingDB].[dbo].[VehicleParkingSlot]
    (VehicleId, ParkingSlotId, EntryDateTime, ExitDateTime)
    VALUES
     (8, 9, GETDATE(), DATEADD(HOUR,1,GETDATE()))                                       --LARGE; FLAT RATE
    ,(1, 2, GETDATE(), DATEADD(HOUR,5,GETDATE()))                                       --SMALL; EXCEED 2 hours
    ,(5, 4, GETDATE(), DATEADD(MINUTE,25,DATEADD(HOUR,3,GETDATE())))                    --MEDIUM; EXCEED 25 minutes
    ,(7, 7, GETDATE(), DATEADD(HOUR,25,GETDATE()))                                      --LARGE; 1 day and 1 hour
    ,(3, 1, GETDATE(), DATEADD(HOUR,28,GETDATE()))                                      --SMALL; 1 day and 4 hours
    ,(4, 5, GETDATE(), DATEADD(HOUR,2,GETDATE()))                                       --MEDIUM; Flat rate
    ,(4, 7, DATEADD(MINUTE,45,DATEADD(HOUR,2,GETDATE())), DATEADD(HOUR,2,DATEADD(MINUTE,45,DATEADD(HOUR,2,GETDATE()))))    --LARGE; RETURNED after 45 minutes (another 2 hours; flatrate + exceed 1 hour)
    --,(2, 3, GETDATE(), DATEADD(HOUR,1,GETDATE()))
    --,(6, 6, GETDATE(), DATEADD(HOUR,1,GETDATE()))
end

