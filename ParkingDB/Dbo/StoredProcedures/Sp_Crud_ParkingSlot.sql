--===================================================================================
--GET ALL					-> @init_flag = 0
--GET BY ID					-> @init_flag = 1
--INSERT					-> @init_flag = 2
--UPDATE					-> @init_flag = 3
--DELETE					-> @init_flag = 4
--===================================================================================
CREATE PROCEDURE [dbo].[Sp_Crud_ParkingSlot]
(
     @init_flag		int
	,@Id			int		= NULL
	,@Size			int	    = NULL
	,@IsOccupied	Bit	    = NULL
)
AS

DECLARE @ERR		int
DECLARE @TABLE_NAME varchar(20)
DECLARE @RESULT		int

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- =================================================================================
	-- 		GET ALL
	-- =================================================================================
	IF @init_flag = 0
	BEGIN
		SELECT *
		FROM [dbo].[ParkingSlot]
	END	

	-- =================================================================================
	-- 		GET BY ID
	-- =================================================================================
	ELSE IF @init_flag = 1
	BEGIN
		
		SELECT TOP(1) *
		FROM [dbo].[ParkingSlot]
		WHERE [Id] = @Id
	END	

	-- =================================================================================
	--		INSERT	
	-- =================================================================================	
	ELSE IF @init_flag = 2
	BEGIN
		INSERT INTO [dbo].[ParkingSlot]
		(	
			[Size]
		) 
		VALUES 
		(	
			@Size			
		)
	END
		
	
	-- =================================================================================
	-- 		UPDATE
	-- =================================================================================
	ELSE IF @init_flag = 3
	BEGIN	
		UPDATE [dbo].[ParkingSlot] SET
			 [Size]         = @Size
            ,[IsOccupied]   = @IsOccupied
		WHERE 
			[Id] = @Id	
	END


	-- =================================================================================
	-- 		DELETE
	-- =================================================================================
	ELSE IF @init_flag = 4
	BEGIN
		DELETE
		FROM [dbo].[ParkingSlot]
		WHERE [Id] = @Id
	
	END	

	RETURN 0		
END

GO


